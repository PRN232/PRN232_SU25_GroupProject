using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRN232_SU25_GroupProject.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTableVisionHearing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisionHearings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisionHearings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HearingLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HearingRight = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastChecked = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalProfileId = table.Column<int>(type: "int", nullable: false),
                    VisionLeft = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisionRight = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisionHearings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisionHearings_MedicalProfiles_MedicalProfileId",
                        column: x => x.MedicalProfileId,
                        principalTable: "MedicalProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VisionHearings_MedicalProfileId",
                table: "VisionHearings",
                column: "MedicalProfileId",
                unique: true);
        }
    }
}
