namespace TopicsApi.Services;

public class RpcDeveloperLookup
{
    private readonly HttpClient _httpClient;

    public RpcDeveloperLookup(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GetCurrentDeveloperModel> GetCurrentDeveloperModelAsync()
    {
        var response = await _httpClient.GetAsync("/");

        var content = await response.Content.ReadFromJsonAsync<GetCurrentDeveloperModel>();

        return content!;
    }
}
