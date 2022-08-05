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

    public async Task<GoogleSearchResult> SearchAsync(string searchPhrase, int pageNumber, int pageSize)
    {
        var searchService = new CustomsearchService(new BaseClientService.Initializer
        {
            ApiKey = _configuration.ApiKey
        });

        var listRequest = searchService.Cse.List();
        listRequest.Cx = _configuration.Cx;
        listRequest.Q = searchPhrase;
        listRequest.Num = pageSize < 10 ? pageSize : 10; // Number of results (cannot be more than 10)
        listRequest.Start = (pageNumber - 1) * pageSize; // Start index

        var results = await listRequest.ExecuteAsync();
        if(results == null)
        {
            throw new ArgumentNullException(nameof(results));
        }

        var items = results.Items?.Select(x => new GoogleSearchResultItem(x.Title, x.Snippet, x.Link)) ?? new List<GoogleSearchResultItem>();
        return new GoogleSearchResult(int.Parse(results.SearchInformation.TotalResults), pageNumber, pageSize, items);
    }
}