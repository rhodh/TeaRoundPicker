using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace Application.HttpExceptions
{
    public class OverOrderLimitException : HttpResponseException
    {
        public OverOrderLimitException(User user) : base(CreatePD(user))
        {
        }

        private static ProblemDetails CreatePD(User user)
        {
            return new ProblemDetails
            {
                Title = "Over Order Limit",
                Type = "OverOrderLimit",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = $"User {user.Id} has gone over the Order limit of 1"
            };
        }
    }
}
