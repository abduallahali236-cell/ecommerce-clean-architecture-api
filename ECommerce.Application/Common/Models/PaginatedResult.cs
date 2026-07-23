using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Models
{
    public class PaginatedResult<T>
    {
        public IReadOnlyCollection<T> Items { get; init; }

        public int PageNumber { get; init; }

        public int PageSize { get; init; }

        public int TotalCount { get; init; }

        public int TotalPages =>
            (int)Math.Ceiling(
                TotalCount /
                (double)PageSize);
    }
}
