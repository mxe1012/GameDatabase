using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GameDatabase.Data
{
    public class AuthorizeCheckOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>().Any()
            || context.MethodInfo.GetCustomAttributes(true)
            .OfType<AuthorizeAttribute>().Any();

            if (hasAuthorize)
        {
            operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    { new OpenApiSecuritySchemeReference("Bearer", context.Document), new List<string>() }
                }
            };
        }
        }
    }
}