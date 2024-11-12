using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base(StatusCodes.Status404NotFound)
        {
        }

        public NotFoundException(string devMessage, string userMessage) : base(StatusCodes.Status404NotFound, devMessage, userMessage)
        {
        }
    }
}
