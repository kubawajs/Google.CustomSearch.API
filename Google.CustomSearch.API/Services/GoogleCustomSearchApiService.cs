using Google.Apis.Customsearch.v1;
using Google.Apis.Services;
using Google.CustomSearch.API.Configuration;
using Microsoft.Extensions.Options;

namespace Google.CustomSearch.API.Services;

internal class GoogleCustomSearchApiService : IGoogleCustomSearchApiService
{
    private readonly GoogleCustomSearchApiConfiguration _configuration;

    public GoogleCustomSearchApiService(IOptions<GoogleCustomSearchApiConfiguration> options)
    {
        _configuration = options.Value;
    }

    public async Task<GoogleSearchResult> SearchAsync(string searchPhrase)
    {
        var searchService = new CustomsearchService(new BaseClientService.Initializer
        {
            ApiKey = _configuration.ApiKey
        });

        var listRequest = searchService.Cse.List();
        listRequest.Cx = _configuration.Cx;
        listRequest.Q = searchPhrase;

        var results = await listRequest.ExecuteAsync();
        if(results == null)
        {
            throw new ArgumentNullException(nameof(results));
        }

        var items = results.Items?.Select(x => new GoogleSearchResultItem(x.Title, x.Snippet, x.Link)) ?? new List<GoogleSearchResultItem>();
        return new GoogleSearchResult(int.Parse(results.SearchInformation.TotalResults), items);
    }
}