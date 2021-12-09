using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
    }
}
