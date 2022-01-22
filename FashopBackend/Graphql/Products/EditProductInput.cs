using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{
    public record EditProductInput(int Id, string Name, string Descriptions, decimal Price, int Sale, int BrandId, IEnumerable<int> CategoryIds, IEnumerable<string> ImageUrls);
}