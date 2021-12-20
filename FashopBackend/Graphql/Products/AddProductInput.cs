using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{
    public record AddProductInput(string Name, int BrandId, IEnumerable<int> CategoryIds);
}
