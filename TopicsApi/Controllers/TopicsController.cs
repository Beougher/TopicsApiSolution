namespace TopicsApi.Controllers;

public class TopicsController : ControllerBase
{
    private readonly IProvideTopicsData _topicsData;

    public TopicsController(IProvideTopicsData topicsData)
    {
        _topicsData = topicsData;
    }

    [HttpPost("topics")]
    public async Task<ActionResult> AddTopicAsync([FromBody] PostTopicRequestModel request)
    {
        // 3. If it is valid
        //      a) do the work (side efect).  (for us, add it to the database), etc.
        GetTopicListItemModel response = await _topicsData.AddTopicAsync(request);
        //      b) return a
        //         201 Created status code.
        //         Add a Location header to the response with the URI of the new resource (Location: http://localhost:1337/topics/3)
        //         Maybe just give them a copy of what they'd get from the URI.

        return CreatedAtRoute("topics.getbyidasync", new { topicId = response.id }, response);
    }

    [HttpDelete]


    [HttpGet("topics/{topicId:int}", Name = "topics.getbyidasync")]
    public async Task<ActionResult> GetTopicByIdAsync(int topicId)
    {
        Maybe<GetTopicListItemModel> response = await _topicsData.GetTopicByIdAsync(topicId);

        return response.hasValue switch
        {
            false => NotFound(),
            true => Ok(response.value)
        };
    }

    [HttpGet("topics")]
    [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 10)]
    public async Task<IActionResult> GetTopicsAsync()
    {
        GetTopicsModel response = await _topicsData.GetAllTopics();

        return Ok(response);
    }
}
