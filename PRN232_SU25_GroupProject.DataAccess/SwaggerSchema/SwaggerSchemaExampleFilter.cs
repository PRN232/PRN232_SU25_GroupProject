﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace PRN232_SU25_GroupProject.DataAccess.SwaggerSchema
{
    public class SwaggerSchemaExampleFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo != null)
            {
                var schemaAttribute = context.MemberInfo.GetCustomAttributes<SwaggerSchemaExampleAttribute>().FirstOrDefault();
                if (schemaAttribute != null)
                    ApplySchemaAttribute(schema, schemaAttribute);
            }
        }

        private void ApplySchemaAttribute(OpenApiSchema schema, SwaggerSchemaExampleAttribute schemaAttribute)
        {
            if (schemaAttribute.Example != null)
            {
                schema.Example = new Microsoft.OpenApi.Any.OpenApiString(schemaAttribute.Example);
            }
        }
    }

}
