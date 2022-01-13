using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.SharedKernel.Shared;
using Microsoft.IdentityModel.Tokens;

namespace FashopBackend.Core.Services;

public class TokenService : ITokenService
{
    public Tokens GenerateToken(int userId, string userEmail, string role, AccessTokenSettings accessTokenSettings, RefreshTokenSettings refreshTokenSettings)
    {
        SymmetricSecurityKey accessSymmetricSecurityKey = new (Encoding.UTF8.GetBytes(accessTokenSettings.Key));
        SymmetricSecurityKey refreshSymmetricSecurityKey = new (Encoding.UTF8.GetBytes(refreshTokenSettings.Key));
        
        SigningCredentials accessCredentials = new (accessSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        SigningCredentials refreshCredentials = new (refreshSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        Claim[] claims = new[] { new Claim(ClaimTypes.Role, role) };
        
        JwtSecurityToken accessJwtToken = new (
            issuer: accessTokenSettings.Issuer,
            audience: accessTokenSettings.Audience,
            expires: DateTime.Now.AddHours(4),
            signingCredentials: accessCredentials,
            claims: claims
        );
        
        JwtSecurityToken refreshJwtToken = new (
            issuer: refreshTokenSettings.Issuer,
            audience: refreshTokenSettings.Audience,
            expires: DateTime.Now.AddMonths(4),
            signingCredentials: refreshCredentials,
            claims: claims
        );
 
        string refreshToken = new JwtSecurityTokenHandler().WriteToken(refreshJwtToken);
        string accessToken = new JwtSecurityTokenHandler().WriteToken(accessJwtToken);

        return new Tokens(accessToken, refreshToken);
    }
}