namespace TopicsApi.Models;

public record Maybe<T>(bool hasValue, T? value);