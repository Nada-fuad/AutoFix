using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Domain.Common.Results
{
   public struct  Error
    {
        public string? ErrorCode { get; set; }
        public string Message { get; set; }


        public ErrorKind Type;

        private Error(string errorCode, string message,  ErrorKind type) {
            ErrorCode = errorCode;

            Message = message;
            Type = type;

        }

        public static Error Failure(string errorCode = nameof(Failure), string message = "General Failure") => new ( errorCode, message, ErrorKind.Failure);
        public static Error Unexpected(string errorCode=nameof(Unexpected),string message = "Unexpected error") => new(errorCode,message, ErrorKind.Unexpected);
        public static Error Validation(string errorCode = nameof(Validation), string message = "Validation error") => new(errorCode, message, ErrorKind.Validation);
        public static Error Conflict(string errorCode=nameof(Conflict), string message = "Conflict error")=> new(errorCode, message, ErrorKind.Conflict);

        public static Error NotFound(string errorCode=nameof(NotFound),string message= "NotFound error")=>new (errorCode,message, ErrorKind.NotFound);  
        public static Error Unauthorized(string errorCode= nameof(Unauthorized),string message= "Unauthorized error")=>new(errorCode,message, ErrorKind.Unauthorized);
        public static Error Forbidden(string errorCode = nameof(Forbidden), string message = "Forbidden error") => new(errorCode, message, ErrorKind.Forbidden);




    }
}
