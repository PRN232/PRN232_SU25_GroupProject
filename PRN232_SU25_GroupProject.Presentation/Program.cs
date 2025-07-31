using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PRN232_SU25_GroupProject.DataAccess.Context;
using PRN232_SU25_GroupProject.DataAccess.Entities;
using PRN232_SU25_GroupProject.DataAccess.SwaggerSchema;
using PRN232_SU25_GroupProject.Presentation.DependencyInjection;
using PRN232_SU25_GroupProject.Presentation.Initialization;
using System.Reflection;
using System.Text;

// Load .env
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);


// === JWT from environment or fallback to appsettings ===
var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? builder.Configuration["JwtSettings:Audience"];
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? builder.Configuration["JwtSettings:SecretKey"];

// === Load env variables ===
builder.Configuration.AddEnvironmentVariables();

// === DB Context ===
builder.Services.AddDbContext<SchoolMedicalDbContext>(options =>
{
    // Lấy từ biến môi trường, nếu không có thì fallback về appsettings
    var connStr = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                 ?? builder.Configuration.GetConnectionString("DefaultConnection");

    options.UseSqlServer(connStr);
});

// === Identity ===
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<SchoolMedicalDbContext>()
    .AddDefaultTokenProviders();

// === Custom services ===
builder.Services.AddApplicationService();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

// === Swagger ===
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Medical Management System API",
        Version = "v1",
        Description = "Hệ thống quản lý y tế học đường (SMMS API) cho phép theo dõi thông tin sức khỏe học sinh, đơn thuốc, hồ sơ y tế, và xử lý các yêu cầu liên quan đến phụ huynh, y tá, quản lý và admin.",
        Contact = new OpenApiContact
        {
            Name = "SMMS Development Team",
            Email = "Quanghuy01062004@gmail.com",
            Url = new Uri("https://schoolmedical.hyudequeue.xyz")
        },
        License = new OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    c.EnableAnnotations();
    c.SchemaFilter<SwaggerSchemaExampleFilter>();

    // Cấu hình bảo mật với JWT Bearer Token
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header. 
Nhập token vào trường sau (ví dụ: Bearer {token}). 
Token được lấy sau khi đăng nhập thành công.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    // Bao gồm XML comment cho Swagger từ file tài liệu XML sinh ra từ /// summary
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});


// === JWT Authentication ===
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

builder.Services.AddAuthorization();

// === CORS ===
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// === Dev Swagger ===
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigrations();

// === Seed DB ===
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<SchoolMedicalDbContext>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    context.Database.Migrate();
    DataSeeder.SeedDatabase(context);
    await DataSeeder.SeedPasswordsAsync(userManager);
}

app.Run();
