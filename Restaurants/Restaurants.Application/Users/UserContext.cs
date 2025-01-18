﻿using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurants.Application.Users;

public class UserContext(IHttpContextAccessor accessor) : IUserContext
{

    public CurrentUser? GetCurrentUser()
    {
        var user = accessor?.HttpContext?.User;

        if(user is null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity is null || !user.Identity.IsAuthenticated) 
        {
            return null;
        }

        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role)!.Select(c => c.Value);
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;

        var dateOfBirthString = user.FindFirst(c => c.Type == "DateOfBirth")?.Value;
        var dateOfBirth = dateOfBirthString == null ? (DateOnly?)null : DateOnly.ParseExact(dateOfBirthString, "yyyy-MM-dd");

        return new CurrentUser(userId, email, roles, nationality, dateOfBirth);


    }
}

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}