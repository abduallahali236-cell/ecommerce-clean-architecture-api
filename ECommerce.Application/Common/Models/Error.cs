using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{

    public sealed record Error(
        string Code,
        string Description,
        ErrorType Type = ErrorType.Failure);

}
