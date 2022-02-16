namespace TopicsApi.Models;

public record Maybe<T>(bool hasValue, T? value);

public record Maybe(bool hasValue);