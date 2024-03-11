using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using Hotel.Query.AzureSearch.Documents;
using Hotel.Query.Data;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Query.AzureSearch.Services;

public class HotelSearchIndexUpdateService : IHotelSearchIndexUpdateService
{
    private readonly SearchIndexClient _searchIndexClient;
    private readonly SearchClient _searchClient;
    private readonly HotelDbContext _dbContext;
    private readonly string _indexName;

    public HotelSearchIndexUpdateService(IConfiguration configuration, HotelDbContext dbContext)
    {
        string searchServiceName = configuration["AzureSearch:ServiceName"] ?? throw new ArgumentNullException("AzureSearch:ServiceName");
        _indexName = configuration["AzureSearch:HotelIndex1Name"] ?? throw new ArgumentNullException("AzureSearch:HotelIndex1Name");
        string apiKey = configuration["AzureSearch:ApiKey"] ?? throw new ArgumentNullException("AzureSearch:ApiKey");

        Uri endpointUri = new Uri($"https://{searchServiceName}.search.windows.net/");
        AzureKeyCredential azureKeyCredential = new AzureKeyCredential(apiKey);
        _searchIndexClient = new SearchIndexClient(endpointUri, azureKeyCredential);
        _searchClient = new SearchClient(endpointUri, _indexName, azureKeyCredential);
        _dbContext = dbContext;
    }

    public async Task UpdateIndexAsync()
    {
        await DeleteIndexAsync(_indexName);
        await CreateIndex(_indexName, _searchIndexClient);
        IEnumerable<HotelSearchDocument> searchDocuments = await ReloadSearchDocumentsFromDb();
        IndexDocumentsBatch<HotelSearchDocument> batch = IndexDocumentsBatch.Create(searchDocuments.Select(i => IndexDocumentsAction.Upload(i)).ToArray());
        // Indexe os documentos no Azure Search
        await _searchClient.IndexDocumentsAsync(batch);
    }

    private async Task<IEnumerable<HotelSearchDocument>> ReloadSearchDocumentsFromDb()
    {
        // Obtenha os dados do banco de dados
        var todos = await _dbContext.Hotels.Include(r=>r.Address).ToListAsync();

        // Mapeie os dados para documentos de índice
        var searchDocuments = todos.Select(dbHotel => new HotelSearchDocument
        {
            HotelId = dbHotel.Id.ToString(),
            HotelName = dbHotel.HotelName,
            Description = dbHotel.Description,
            DescriptionFr = dbHotel.DescriptionFr,
            Category = dbHotel.Category,
            Tags = dbHotel.Tags,
            ParkingIncluded = dbHotel.ParkingIncluded,
            LastRenovationDate = dbHotel.LastRenovationDate,
            Rating = dbHotel.Rating,
            Address = new AddressSearchDocument
            {
                StreetAddress = dbHotel.Address.StreetAddress,
                City = dbHotel.Address.City,
                Country = dbHotel.Address.Country,
                PostalCode = dbHotel.Address.PostalCode,
                StateProvince = dbHotel.Address.StateProvince
            }
        });
        return searchDocuments;
    }

    private async Task CreateIndex(string indexName, SearchIndexClient adminClient)
    {
        FieldBuilder fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(HotelSearchDocument));

        var definition = new SearchIndex(indexName, searchFields);

        var suggester = new SearchSuggester("sg", new[] { "HotelName", "Category", "Address/City", "Address/StateProvince" });
        definition.Suggesters.Add(suggester);

        await adminClient.CreateOrUpdateIndexAsync(definition);
    }

    public async Task DeleteIndexAsync(string indexName)
    {
        try
        {
            await _searchIndexClient.DeleteIndexAsync(indexName);
            Console.WriteLine($"Index '{indexName}' deleted successfully.");
        }
        catch (RequestFailedException ex)
        {
            Console.WriteLine($"Failed to delete index '{indexName}': {ex.Message}");
        }
    }
}
