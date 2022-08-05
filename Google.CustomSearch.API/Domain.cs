namespace Google.CustomSearch.API;

public record GoogleSearchResult(int TotalResults, IEnumerable<GoogleSearchResultItem> Items);
public record GoogleSearchResultItem(string Headline, string Description, string Url);