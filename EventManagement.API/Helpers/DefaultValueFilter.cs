using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel;
using System.Reflection;

namespace EventManagement.API.Helpers
{
    public class DefaultValueFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.IsClass)
            {
                foreach (var property in context.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var defaultValue = property.GetCustomAttribute<DefaultValueAttribute>();
                    if (defaultValue != null)
                    {
                        var propertyName = char.ToLowerInvariant(property.Name[0]) + property.Name.Substring(1);
                        var propertySchema = schema.Properties[propertyName];

                        propertySchema.Default = new OpenApiString(defaultValue.Value.ToString());
                    }
                }
            }
        }
    }
}
