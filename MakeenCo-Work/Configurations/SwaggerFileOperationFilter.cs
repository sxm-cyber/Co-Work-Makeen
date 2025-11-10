using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace MakeenCo_Work.Configurations
{
    public class SwaggerFileOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile) ||
                            p.ParameterType == typeof(IFormFile[]) ||
                            p.ParameterType == typeof(List<IFormFile>) ||
                            p.ParameterType == typeof(IEnumerable<IFormFile>))
                .ToList();

            var hasFromFormAttribute = context.MethodInfo.GetParameters()
                .Any(p => p.GetCustomAttributes<FromFormAttribute>().Any());

            if (!formFileParams.Any() && !hasFromFormAttribute)
                return;

            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>(),
                            Required = new HashSet<string>()
                        }
                    }
                }
            };

            var schema = operation.RequestBody.Content["multipart/form-data"].Schema;

            foreach (var param in context.MethodInfo.GetParameters())
            {
                if (param.GetCustomAttributes<FromFormAttribute>().Any())
                {
                    if (param.ParameterType == typeof(IFormFile) ||
                        param.ParameterType == typeof(List<IFormFile>) ||
                        param.ParameterType == typeof(IFormFile[]))
                    {
                        schema.Properties.Add(param.Name!, new OpenApiSchema
                        {
                            Type = "string",
                            Format = "binary"
                        });
                    }
                    else if (param.ParameterType.IsClass && param.ParameterType != typeof(string))
                    {
                        foreach (var prop in param.ParameterType.GetProperties())
                        {
                            var propType = "string";
                            string? propFormat = null;

                            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
                                propType = "integer";
                            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
                                propType = "boolean";

                            schema.Properties.Add(prop.Name, new OpenApiSchema
                            {
                                Type = propType,
                                Format = propFormat
                            });

                            if (!IsNullable(prop.PropertyType))
                            {
                                schema.Required.Add(prop.Name);
                            }
                        }
                    }
                }
            }
        }

        private bool IsNullable(Type type)
        {
            return Nullable.GetUnderlyingType(type) != null ||
                   !type.IsValueType ||
                   (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
        }
    }
}