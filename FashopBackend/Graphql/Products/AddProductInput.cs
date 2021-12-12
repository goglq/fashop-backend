using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{
    public record AddProductInput(string Name, IEnumerable<int> CategoryIds);
}
