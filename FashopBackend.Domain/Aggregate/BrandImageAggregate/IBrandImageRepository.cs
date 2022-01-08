using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.BrandImageAggregate;

public interface IBrandImageRepository : IRepository<BrandImage>
{
    BrandImage GetByBrandId(int brandId);
}