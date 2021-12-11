using FashopBackend.Core.Aggregate.ProductAggregate;
using System.Collections.Generic;

namespace FashopBackend.Graphql.Categories
{
    public record EditCategoryInput(int Id, string Name, IEnumerable<int> ProductIds);
}
