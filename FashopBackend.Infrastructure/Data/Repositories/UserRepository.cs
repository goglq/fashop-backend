using FashopBackend.Core.Aggregate.UserAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(FashopContext context) : base(context)
    {
        
    }
}