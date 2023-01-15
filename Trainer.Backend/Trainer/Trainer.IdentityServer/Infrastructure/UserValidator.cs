using System.Security.Claims;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Interfaces;
using Trainer.Common;
using Trainer.Common.TableConnect.Common;

namespace Trainer.IdentityServer.Infrastructure;

public class UserValidator : IResourceOwnerPasswordValidator
{
    private readonly ITrainerDbContext _dbContext;

    public UserValidator(ITrainerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _dbContext.BaseUsers.FirstOrDefaultAsync(x => x.Email == context.UserName);

        if (user != null)
        {
            var result = CryptoHelper.VerifyHashedPassword(user.PasswordHash, context.Password);
            // context set to success
            context.Result = new GrantValidationResult(
                subject: user.Id.ToString(),
                authenticationMethod: "custom",
                claims: new Claim[] {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToName())
                }
            );

            return;
        }

        // context set to Failure        
        context.Result = new GrantValidationResult(
            TokenRequestErrors.UnauthorizedClient, "Invalid Credentials");
        
        return;
    }
}