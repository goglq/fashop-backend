using System.Collections.Generic;
using FashopBackend.Core.Aggregate.ProductAggregate;

namespace FashopBackend.Graphql.Carts;

public record AddCartInput(int Count, IEnumerable<int> ProductIds);