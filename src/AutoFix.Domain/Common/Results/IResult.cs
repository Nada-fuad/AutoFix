using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Domain.Common.Results
{
    public interface IResult
    {
         List<Error>? Errors { get; }
        bool IsSuccess { get; }
    }

    public interface IResult<out TValue>:IResult
    {
       TValue Value { get; }
    }
}
