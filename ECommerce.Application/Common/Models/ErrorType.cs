using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{

    public enum ErrorType
    {
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure
    }
}
