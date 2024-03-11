using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Infrastructure.Extensions;

class ODataParametersSwaggerDefinition : IOperationFilter
{
    static List<OpenApiParameter> s_Parameters = (new List<(string Name, string Description)>()
            {
                ( "$top", "The max number of records."),
                ( "$skip", "The number of records to skip."),
                ( "$filter", "A function that must evaluate to true for a record to be returned."),
                ( "$select", "Specifies a subset of properties to return. Use a comma separated list."),
                ( "$orderby", "Determines what values are used to order a collection of records."),
                ( "$expand", "Use to add related query data.")
            }).Select(pair => new OpenApiParameter
            {
                Name = pair.Name,
                Required = false,
                Schema = new OpenApiSchema { Type = "String" },
                In = ParameterLocation.Query,
                Description = pair.Description,

            }).ToList();


    private static readonly Type QueryableType = typeof(IQueryable);

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var isTypeOfODataMetadata = context.ApiDescription.ActionDescriptor.EndpointMetadata.Any(em => em is Microsoft.AspNetCore.OData.Routing.ODataRoutingMetadata);
         var hasNoParams = (operation.Parameters == null || operation.Parameters.Count == 0);
         var teste = context.MethodInfo.ReturnType.GetInterfaces().ToList();
         var isMetadata = context.ApiDescription.ActionDescriptor.AttributeRouteInfo.Name.Contains("$metadata");
;
        var isQueryable = context.MethodInfo.ReturnType.GetInterfaces().Any(i => i == QueryableType);

        if (isTypeOfODataMetadata && !isMetadata && hasNoParams)
        {
            operation.Parameters ??= new List<OpenApiParameter>();
            foreach (var item in s_Parameters)
                operation.Parameters.Add(item);
        }
    }
}