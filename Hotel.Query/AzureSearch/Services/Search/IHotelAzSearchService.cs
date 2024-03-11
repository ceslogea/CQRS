using Hotel.Query.AzureSearch.Documents;

namespace Hotel.Query.AzureSearch.Services;

public interface IHotelAzSearchService
{

    Task<IEnumerable<HotelSearchDocument>> SearchAsync(string searchText);
}
