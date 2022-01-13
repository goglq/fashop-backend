using FashopBackend.Core.Aggregate.CartAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Infrastructure.Data.Repositories
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(FashopContext context) : base(context)
        {
        }
    }
}
