using Azure;
using Azure.Search.Documents;
using Hotel.Query.AzureSearch.Documents;

namespace Hotel.Query.AzureSearch.Services;
public class HotelAzSearchService : IHotelAzSearchService
{    
    private readonly SearchClient _searchClient;

    public HotelAzSearchService(IConfiguration configuration)
    {
        string searchServiceName = configuration["AzureSearch:ServiceName"] ?? throw new ArgumentException("AzureSearch:ServiceName");
        string indexName = configuration["AzureSearch:HotelIndex1Name"] ?? throw new ArgumentException("AzureSearch:HotelIndex1Name");
        string apiKey = configuration["AzureSearch:ApiKey"] ?? throw new ArgumentException("AzureSearch:ApiKey");

        Uri endpointUri = new Uri($"https://{searchServiceName}.search.windows.net/");
        _searchClient = new SearchClient(endpointUri, indexName, new AzureKeyCredential(apiKey));
    }
    public async Task<IEnumerable<HotelSearchDocument>> SearchAsync(string searchText)
    {
       // Configure a pesquisa
        var options = new SearchOptions
        {
            IncludeTotalCount = true,
            // Defina os campos que deseja destacar nos resultados da pesquisa
            // Select = new[] { "Id", "Name", "IsComplete" } 
        };

        // Realize a pesquisa
        var searchResults = await _searchClient.SearchAsync<HotelSearchDocument>(searchText, options);

        // Retorne os resultados
        return searchResults.Value.GetResults().Select(r => r.Document);
    }

}
