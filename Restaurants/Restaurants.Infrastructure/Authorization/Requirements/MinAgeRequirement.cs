using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }

        public MinAgeRequirement(int minimumAge)
        {
            MinimumAge = minimumAge;
        }
    }
}
