using System.Collections.Generic;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.UserAggregate;

public interface IUserRepository : IRepository<User>
{
    User GetUserByToken(string token);

    User GetUserByEmail(string email, bool includeRole = false);

    List<User> GetUsersWithRoles();
}