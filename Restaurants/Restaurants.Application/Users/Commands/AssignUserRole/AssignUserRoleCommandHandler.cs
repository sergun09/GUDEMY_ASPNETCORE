﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
    public class AssignUserRoleCommandHandler(ILogger<AssignUserRoleCommandHandler> logger, 
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        : IRequestHandler<UnAssignUserRoleCommand>
    {
        public async Task Handle(UnAssignUserRoleCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Assigning role {@Request}", request);
            var user = await userManager.FindByEmailAsync(request.UserEmail);

            if (user is null)
                throw new NotFoundException(nameof(User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName);

            if (role is null)
                throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await userManager.AddToRoleAsync(user, role.Name!);
        }
    }
}
