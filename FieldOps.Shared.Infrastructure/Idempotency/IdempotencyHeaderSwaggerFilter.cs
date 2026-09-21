using IdempotentAPI.Filters;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Nodes;

namespace FieldOps.Shared.Infrastructure.Idempotency
{
    public class IdempotencyHeaderSwaggerFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var isIdempotent = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<IdempotentAttribute>()
                .Any() ||
                (context.MethodInfo.DeclaringType?.GetCustomAttributes(true).OfType<IdempotentAttribute>().Any() ?? false);

            if (isIdempotent)
            {
                operation.Parameters ??= new List<IOpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "IdempotencyKey",
                    In = ParameterLocation.Header,
                    Required = false, 
                    Schema = new OpenApiSchema
                    {
                        Type = JsonSchemaType.String,
                        Default = JsonValue.Create(Guid.NewGuid().ToString())
                    },
                    Description = "Unique key for idempotency (GUID) to prevent request duplication."
                });
            }
        }
    }
}
