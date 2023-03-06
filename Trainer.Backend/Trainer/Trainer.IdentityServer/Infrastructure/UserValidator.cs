using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Trainer.Application.Interfaces;
using Trainer.Common;
using Trainer.Common.TableConnect.Common;
using Trainer.Enums;
using Trainer.IdentityServer.Models;

namespace Trainer.IdentityServer.Infrastructure;

public class UserValidator : IResourceOwnerPasswordValidator
{
    private readonly ITrainerDbContext _dbContext;
    private readonly SuperAdmin _supAdmin;
    public UserValidator(ITrainerDbContext dbContext, IOptions<SuperAdmin> superAdmin)
    {
        _dbContext = dbContext;
        _supAdmin = superAdmin.Value;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        if (context.UserName == _supAdmin.Email && context.Password == _supAdmin.Password)
        {
            context.Result = new GrantValidationResult(
                subject: _supAdmin.Email,
                authenticationMethod: "custom",
                claims: new[]
                {
                    new Claim(ClaimTypes.Name, _supAdmin.Email),
                    new Claim(ClaimTypes.NameIdentifier, _supAdmin.Email),
                    new Claim(ClaimTypes.Role, UserRole.SuperAdmin.ToName())
                }
            );
            return;
        }
            
            
        var user = await _dbContext.BaseUsers.FirstOrDefaultAsync(x => x.Email == context.UserName);

        if (user != null)
        {
            var result = CryptoHelper.VerifyHashedPassword(user.PasswordHash, context.Password);
            if (result)
            {
                // context set to success
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "custom",
                    claims: new[]
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToName())
                    }
                );
                return;
            }
        }

        // context set to Failure        
        context.Result = new GrantValidationResult(
            TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
    }
}