using FashopBackend.Core.Aggregate.BrandAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
{
    public BrandRepository(FashopContext context) : base(context)
    {
    }
}