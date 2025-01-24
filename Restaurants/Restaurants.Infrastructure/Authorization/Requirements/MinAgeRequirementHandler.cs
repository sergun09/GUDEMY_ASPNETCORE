using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements;

public class MinAgeRequirementHandler(ILogger<MinAgeRequirementHandler> logger, IUserContext userContext) 
    : AuthorizationHandler<MinAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinAgeRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        logger.LogInformation("User : {Email}, date of birth {Dob} - Handling MinAgeRequirementHandler",
            currentUser!.Email, currentUser.DateOfBirth);

        if(currentUser.DateOfBirth is null) 
        {
            logger.LogWarning("User Date Of Birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today)) 
        {
            logger.LogInformation("Authorization succedeed");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
