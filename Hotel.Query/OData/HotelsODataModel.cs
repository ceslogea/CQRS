using Hotel.Query.Data;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;

namespace Hotel.Query.OData;

public class HotelsODataModel
{
     public static void BuildODataModel(WebApplicationBuilder builder)
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EntityType<HotelEntity>();
        modelBuilder.EntitySet<AddressEntity>("Address");

        builder.Services.AddControllers().AddOData(
            options => options.EnableQueryFeatures().Select().Filter().OrderBy().Expand().Count().SetMaxTop(100).AddRouteComponents(
                "hotel-odata",
                modelBuilder.GetEdmModel()));
    }
}
