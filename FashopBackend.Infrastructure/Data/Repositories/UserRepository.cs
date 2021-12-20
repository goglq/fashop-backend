using System.Collections.Generic;
using System.Linq;
using FashopBackend.Core.Aggregate.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(FashopContext context) : base(context)
    {
        
    }

    public User GetUserByToken(string token)
    {
        User user = DbSet.Include(p => p.Role).FirstOrDefault(u => u.Token == token);
        return user;
    }

    public User GetUserByEmail(string email, bool includeRole = false)
    {
        User user = null;

        if (includeRole)
            user = DbSet.Include(p => p.Role).FirstOrDefault(u => u.Email == email);
        else
            user = DbSet.FirstOrDefault(u => u.Email == email);
        
        return user;
    }

    public List<User> GetUsersWithRoles()
    {
        return DbSet.Include(u => u.Role).ToList();
    }
}