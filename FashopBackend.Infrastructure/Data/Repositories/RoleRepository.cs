using FashopBackend.Core.Aggregate.RoleAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(FashopContext context) : base(context)
    {
        
    }
}