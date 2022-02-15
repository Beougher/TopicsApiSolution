namespace TopicsApi.Services;

public class FakeDeveloperLookup : ILookupOnCallDevelopers
{
    public async Task<GetCurrentDeveloperModel> GetCurrentOnCallDevloperAsync()
    {
        return new GetCurrentDeveloperModel("Joe Schmidt", "777-1212","joe@gmail.com", DateTime.Now);
    }
}
