using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.BrandAggregate;

public interface IBrandRepository : IRepository<Brand>
{
    public Brand GetByName(string name);
}