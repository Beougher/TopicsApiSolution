namespace TopicsApi.Controllers;

[Produces("application/json")]
public class TopicsController : ControllerBase
{
    private readonly IProvideTopicsData _topicsData;

    public TopicsController(IProvideTopicsData topicsData)
    {
        _topicsData = topicsData;
    }

    [HttpPut("topics/{id:int}")]
    public async Task<IActionResult> ReplaceTopicAsync(int id, [FromBody] TopicListItemModel request)
    {
        if(id != int.Parse(request.id))
        {
            return BadRequest("Back off l337 Haxxor!!");
        }
        Maybe response = await _topicsData.ReplaceAsync(id, request);

        return response.hasValue switch
        {
            true => NoContent(),
            false => NotFound()
        };
    }

    [HttpPost("topics")]
    public async Task<ActionResult> AddTopicAsync([FromBody] PostTopicRequestModel request)
    {
        // 3. If it is valid
        //      a) do the work (side efect).  (for us, add it to the database), etc.
        TopicListItemModel response = await _topicsData.AddTopicAsync(request);
        //      b) return a
        //         201 Created status code.
        //         Add a Location header to the response with the URI of the new resource (Location: http://localhost:1337/topics/3)
        //         Maybe just give them a copy of what they'd get from the URI.

        return CreatedAtRoute("topics.getbyidasync", new { topicId = response.id }, response);
    }

    [HttpDelete("topics/{topicId:int}")]
    public async Task<ActionResult> DeleteTopicAsync(int topicId)
    {
        await _topicsData.RemoveAsync(topicId);
        return NoContent();
    }


    [HttpGet("topics/{topicId:int}", Name = "topics.getbyidasync")]
    public async Task<ActionResult> GetTopicByIdAsync(int topicId)
    {
        Maybe<TopicListItemModel> response = await _topicsData.GetTopicByIdAsync(topicId);

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
