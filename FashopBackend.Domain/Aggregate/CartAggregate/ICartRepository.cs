using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.CartAggregate
{
    public interface ICartRepository : IRepository<Cart>
    {
    }
}
