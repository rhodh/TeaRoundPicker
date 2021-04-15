using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;

namespace Application.HttpExceptions
{
    public class OrderNotDefinedException : HttpResponseException
    {
        public OrderNotDefinedException(IEnumerable<Guid> userIds) : base(CreatePD(userIds))
        {
        }

        private static ProblemDetails CreatePD(IEnumerable<Guid> userId)
        {
            return new ProblemDetails
            {
                Title = "Order Not Defined",
                Type = "OrderNotDefined",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = $"Orders Not Defined for Users '{string.Join(", ", userId)}'"
            };
        }
    }
}
