using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{
    public record EditProductInput(int Id, string Name, string Descriptions, double Price, int BrandId, IEnumerable<int> CategoryIds, IEnumerable<string> ImageUrls);
}