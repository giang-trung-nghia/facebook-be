using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Facebook.Domain.Exceptions
{
    public class BaseException : Exception
    {

        #region Properties
        public int ErrorCode { get; set; }

        public string? DevMessage { get; set; }

        public string? UserMessage { get; set; }

        public string? TraceId { get; set; }

        public string? MoreInfo { get; set; }

        public object? Error { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion

        public BaseException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        public BaseException(int errorCode, string userMessage)
        {
            ErrorCode = errorCode;
            DevMessage = userMessage;
            UserMessage = userMessage;
        }

        public BaseException(int errorCode, string devMessage, string userMessage)
        {
            ErrorCode = errorCode;
            DevMessage = devMessage;
            UserMessage = userMessage;
        }
    }
}
