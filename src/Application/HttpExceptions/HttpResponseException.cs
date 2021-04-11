using Microsoft.AspNetCore.Mvc;
using System;

namespace Application.HttpExceptions
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(ProblemDetails problemDetails)
        {
            ProblemDetails = problemDetails;
        }

        public ProblemDetails ProblemDetails { get; }
    }
}
