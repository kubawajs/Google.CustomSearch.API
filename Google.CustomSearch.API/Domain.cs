namespace Google.CustomSearch.API;

public record GoogleSearchResult(int TotalResults, int pageNumber, int pageSize, IEnumerable<GoogleSearchResultItem> Items);
public record GoogleSearchResultItem(string Headline, string Description, string Url);