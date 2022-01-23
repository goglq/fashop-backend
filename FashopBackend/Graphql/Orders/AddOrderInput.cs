using System.Collections.Generic;

namespace FashopBackend.Graphql.Orders;

public record AddOrderInput(string City, string Street, string Building, 
    string Section, string Housing, string PostIndex, 
    string Name, string Surname);