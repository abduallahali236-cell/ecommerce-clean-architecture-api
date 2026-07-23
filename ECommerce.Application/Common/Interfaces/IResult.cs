using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Interfaces
{
    public interface IResult
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        IReadOnlyList<Error> Errors { get; }
    }
}
