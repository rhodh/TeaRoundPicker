using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace Application.HttpExceptions
{
    public class UsersNotFoundExceptions : HttpResponseException
    {
        public UsersNotFoundExceptions(IEnumerable<Guid> userIds) : base(CreatePD(userIds))
        {
        }

        private static ProblemDetails CreatePD(IEnumerable<Guid> userIds)
        {
            return new ProblemDetails
            {
                Title = "Users Not Found",
                Type = "UsersNotFound",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = $"Users not found: '{string.Join(", ", userIds)}'"
            };
        }
    }
}
