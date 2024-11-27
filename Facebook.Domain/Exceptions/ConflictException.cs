using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Exceptions
{
    public class ConflictException : BaseException
    {
        public ConflictException() : base(StatusCodes.Status409Conflict)
        {
        }
        public ConflictException(string userMessage) : base(StatusCodes.Status409Conflict, userMessage)
        {
        }

        public ConflictException(string devMessage, string userMessage) : base(StatusCodes.Status409Conflict, devMessage, userMessage)
        {
        }
    }
}
