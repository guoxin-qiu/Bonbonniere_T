using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Bonbonniere.WebApis.Utils
{
    public class SwaggerHttpHeaderOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            var actionAttrs = context.ApiDescription.ActionAttributes();
            var isAllowAnonymous = actionAttrs.Any(a => a.GetType() == typeof(AllowAnonymousAttribute));

            if (!isAllowAnonymous)
            {
                var controllerAttrs = context.ApiDescription.ControllerAttributes();
                isAllowAnonymous = controllerAttrs.Any(c => c.GetType() == typeof(AllowAnonymousAttribute));
            }

            if (!isAllowAnonymous)
            {
                operation.Parameters.Add(new NonBodyParameter()
                {
                    Name = "Authorization",
                    In = "header",
                    Type = "string",
                    Required = true
                });
            }
        }
    }
}
