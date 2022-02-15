namespace TopicsApi.Models;

public record GetTopicListItemModel(string id, string name, string description);

public record GetTopicsModel(IEnumerable<GetTopicListItemModel> data);