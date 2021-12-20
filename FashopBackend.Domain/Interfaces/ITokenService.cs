using System.Collections.Generic;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.SharedKernel.Shared;

namespace FashopBackend.Core.Interfaces;

public interface ITokenService
{
    Tokens GenerateToken(int userId, string userEmail, string userRole, AccessTokenSettings accessTokenSettings, RefreshTokenSettings refreshTokenSettings);
}