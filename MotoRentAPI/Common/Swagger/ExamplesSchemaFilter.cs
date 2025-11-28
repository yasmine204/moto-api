using System;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MotoRentAPI.Common.Swagger
{
    public class ExamplesSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null)
                return;

            if (ExamplesDictionary.Examples.TryGetValue(context.Type, out var example))
            {
                schema.Example = example;
            }
        }
    }
}
