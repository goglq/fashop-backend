using System.Collections.Generic;

namespace FashopBackend.Graphql.Products
{
    public record AddProductInput(string Name, string Descriptions, double Price, int Sale, int BrandId, IEnumerable<int> CategoryIds, IEnumerable<string> ImageUrls);
}
