using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.EntityFrameworkCore;
using Trainer.Application.Interfaces;
using Trainer.Common;

namespace Trainer.IdentityServer.Infrastructure;

public class ProfileService : IProfileService
{
    private readonly ITrainerDbContext _dbContext;

    public ProfileService(ITrainerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.BaseUsers.FirstOrDefaultAsync(x => x.Email == email);

        var claims = new List<Claim>
        {
            new Claim(JwtClaimTypes.Subject, user.Id.ToString()),
            new Claim(JwtClaimTypes.Role, user.Role.ToName()),
            new Claim(JwtClaimTypes.Email, user.Email),
        };

        context.IssuedClaims.AddRange(claims);
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        //>Processing
        var email = context.Subject.Identity.Name;
        var user = await _dbContext.BaseUsers.FirstOrDefaultAsync(x => x.Email == email);

        context.IsActive = user is {RemovedAt: null};
    }
}