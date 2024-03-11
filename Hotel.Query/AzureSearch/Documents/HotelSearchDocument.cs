using System.Text.Json.Serialization;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace Hotel.Query.AzureSearch.Documents;

public partial class HotelSearchDocument
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string HotelId { get; set; }

    [SearchableField(IsSortable = true)]
    public required string HotelName { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.EnLucene)]
    public required string Description { get; set; }

    [SearchableField(AnalyzerName = LexicalAnalyzerName.Values.FrLucene)]
    [JsonPropertyName("Description_fr")]
    public required string DescriptionFr { get; set; }

    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public required string Category { get; set; }

    [SearchableField(IsFilterable = true, IsFacetable = true)]
    public required string[] Tags { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public bool? ParkingIncluded { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public DateTimeOffset? LastRenovationDate { get; set; }

    [SimpleField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public double? Rating { get; set; }

    [SearchableField]
    public required AddressSearchDocument Address { get; set; }
}

public partial class AddressSearchDocument
{
    [SearchableField(IsFilterable = true)]
    public required string StreetAddress { get; set; }
    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public required string City { get; set; }
    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public required string StateProvince { get; set; }
    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public required string PostalCode { get; set; }
    [SearchableField(IsFilterable = true, IsSortable = true, IsFacetable = true)]
    public required string Country { get; set; }
}
