using System.Collections.Generic;

namespace FashopBackend.Graphql.Orders;

public record AddOrderInput(string Status, string Address, int Count, IEnumerable<int> ProductIds);