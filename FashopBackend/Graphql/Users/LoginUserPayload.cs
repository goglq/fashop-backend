using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;

namespace FashopBackend.Graphql.Users;

public record LoginUserPayload(Tokens Tokens);