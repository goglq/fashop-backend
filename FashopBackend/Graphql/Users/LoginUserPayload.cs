using FashopBackend.Core.Aggregate.UserAggregate;

namespace FashopBackend.Graphql.Users;

public record LoginUserPayload(string Token);