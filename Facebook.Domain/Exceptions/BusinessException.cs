using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Domain.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException() : base(StatusCodes.Status500InternalServerError)
        {
        }

        public BusinessException(string devMessage, string userMessage) : base(StatusCodes.Status500InternalServerError, devMessage, userMessage)
        {
        }
    }
}
