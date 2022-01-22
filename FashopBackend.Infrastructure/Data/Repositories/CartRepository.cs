using FashopBackend.Core.Aggregate.CartAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FashopBackend.Infrastructure.Data.Repositories
{
    public class CartRepository : RepositoryBase<Cart>, ICartRepository
    {
        public CartRepository(FashopContext context) : base(context)
        {
        }

        public Cart  GetCartByUserAndProductId(int userId, int productId)
        {
            Cart cart = DbSet.FirstOrDefault(cart => cart.UserId == userId && cart.ProductId == productId);
            return cart;
        }

        public IEnumerable<Cart> GetAllIncluded(Expression<Func<Cart, bool>> filter = null)
        {
            return filter is null ? 
                DbSet
                    .Include(cart => cart.Product)
                    .Include(cart => cart.User)
                    .ToList() : 
                DbSet
                    .Include(cart => cart.Product)
                    .Include(cart => cart.User)
                    .Where(filter)
                    .ToList();
        }
    }
}
