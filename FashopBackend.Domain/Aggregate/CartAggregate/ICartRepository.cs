using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.CartAggregate
{
    public interface ICartRepository : IRepository<Cart>
    {
        Cart GetCartByUserAndProductId(int userId, int productId);

        IEnumerable<Cart> GetAllIncluded(Expression<Func<Cart, bool>> filter = null);
    }
}
