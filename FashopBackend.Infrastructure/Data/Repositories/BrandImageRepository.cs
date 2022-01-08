using System.Linq;
using FashopBackend.Core.Aggregate.BrandImageAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class BrandImageRepository : RepositoryBase<BrandImage>, IBrandImageRepository
{
    public BrandImageRepository(FashopContext context) : base(context)
    {
        
    }

    public BrandImage GetByBrandId(int brandId)
    {
        return DbSet.FirstOrDefault(brandImage => brandImage.Id == brandId);
    }
}