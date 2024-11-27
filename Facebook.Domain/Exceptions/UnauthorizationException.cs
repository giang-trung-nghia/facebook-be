using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Exceptions
{
    public class UnauthorizationException : BaseException
    {
        public UnauthorizationException() : base(StatusCodes.Status401Unauthorized)
        {
        }

        public UnauthorizationException(string userMessage) : base(StatusCodes.Status401Unauthorized, userMessage, userMessage)
        {
        }

        public UnauthorizationException(string devMessage, string userMessage) : base(StatusCodes.Status401Unauthorized, devMessage, userMessage)
        {
        }
    }
}
