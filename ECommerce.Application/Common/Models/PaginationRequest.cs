using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{
    public abstract record PaginationRequest(
        int PageNumber = 1,
        int PageSize = 10);
}
