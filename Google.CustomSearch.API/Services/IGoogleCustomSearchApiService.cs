namespace Google.CustomSearch.API.Services
{
    public interface IGoogleCustomSearchApiService
    {
        Task<GoogleSearchResult> SearchAsync(string searchPhrase);
    }
}
