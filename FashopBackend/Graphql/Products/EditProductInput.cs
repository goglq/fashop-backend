using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{

    public record EditProductInput(int Id, string Name, int BrandId, IEnumerable<int> CategoryIds);
}