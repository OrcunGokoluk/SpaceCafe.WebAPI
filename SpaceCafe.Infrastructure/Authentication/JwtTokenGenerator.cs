using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpaceCafe.Application.Common.Interfaces.Authentication;
using SpaceCafe.Application.Common.Interfaces.Services;
using SpaceCafe.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpaceCafe.Infrastructure.Authentication;
public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSettings _jwtsettings;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> options)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtsettings = options.Value;
    }

    public string GenerateToken(User user)
    {

        //signing credential is actual token string
        var siginingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtsettings.Secret)),
                SecurityAlgorithms.HmacSha256);


        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
            new Claim(JwtRegisteredClaimNames.UniqueName,Guid.NewGuid().ToString())
        };


        var securityToken = new JwtSecurityToken(
            issuer: _jwtsettings.Issuer,
            audience: _jwtsettings.Audience,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtsettings.ExpiryMinutes),
            claims: claims,
            signingCredentials: siginingCredentials);


        string spacetoken = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return spacetoken;
    }

}
