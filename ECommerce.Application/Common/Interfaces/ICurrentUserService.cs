using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }

        string? Email { get; }

        bool IsAuthenticated { get; }
    }
}
