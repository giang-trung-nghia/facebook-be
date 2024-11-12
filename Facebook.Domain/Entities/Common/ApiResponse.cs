using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facebook.Application.Dtos.Common
{
    public class ApiResponse
    {
        public int Code { get; set; }
        public string? UserMessage { get; set; }
        public string? DevMessage { get; set; }

        public ApiResponse(int code) {
            Code = code;
        }

        public ApiResponse(int code, string userMessage, string devMessage) {
            Code = code;
            UserMessage = userMessage;
            DevMessage = devMessage;
        }
    }
}
