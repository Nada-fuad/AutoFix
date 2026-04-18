using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Domain.Common.Results
{
   public readonly record struct Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorKind Kind { get; }

        private Error(string code, string message, ErrorKind kind)
        {
            Code = code; Message = message; Kind = kind;    
        }


        public static Error Failure(string code=nameof(Failure), string message="General failure")=>new (code , message,ErrorKind.Failure);
        public static Error Unexpected(string code=nameof(Unexpected),string message="Unexpected error")=>new (code , message,ErrorKind.Unexpected);
        public static Error Validation(string code=nameof(Validation), string message= "Validation error")=>new (code , message,ErrorKind.Validation);

        public static Error Conflict(string code=nameof(Conflict), string message= "Conflict error")=>new (code , message,ErrorKind.Conflict);

        public static Error NotFound(string code=nameof(NotFound),string message="NotFound error")=>new (code, message,ErrorKind.NotFound);

        public static Error Unauthorized(string code=nameof(Unauthorized),string message= "Unauthorized error")=>new (code,message,ErrorKind.Unauthorized);

        public static Error Forbidden(string code=nameof(Forbidden),string message= "Forbidden error")=>new Error(code,message,ErrorKind.Forbidden);


        public static Error Create(int kind, string code, string message) => new(code, message, (ErrorKind)kind);
        

    }
}
