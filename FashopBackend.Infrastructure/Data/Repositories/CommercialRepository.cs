using FashopBackend.Core.Aggregate.CommercialAggregate;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class CommercialRepository : RepositoryBase<Commercial>, ICommercialRepository
{
    public CommercialRepository(FashopContext context) : base(context)
    {
    }
}