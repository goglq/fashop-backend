using FashopBackend.Core.Aggregate.ProductAggregate;
using System.Collections.Generic;

namespace FashopBackend.Graphql.Categories
{
    public record EditCategoryInput(int id, string name, IEnumerable<int> productIds);
}
