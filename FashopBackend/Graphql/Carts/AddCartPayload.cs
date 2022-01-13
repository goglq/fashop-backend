using System.Collections.Generic;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;

namespace FashopBackend.Graphql.Carts;

public record AddCartPayload(IEnumerable<Cart> cart);