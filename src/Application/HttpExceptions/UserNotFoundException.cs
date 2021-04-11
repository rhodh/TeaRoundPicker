using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Application.HttpExceptions
{
    public class UserNotFoundException : HttpResponseException
    {
        public UserNotFoundException(Guid userId) : base(CreatePD(userId))
        {
        }

        private static ProblemDetails CreatePD(Guid userId)
        {
            return new ProblemDetails
            {
                Title = "User Not Found",
                Type = "UserNotFound",
                Status = (int)HttpStatusCode.NotFound,
                Detail = $"User with id {userId} not found"
            };
        }
    }
}
