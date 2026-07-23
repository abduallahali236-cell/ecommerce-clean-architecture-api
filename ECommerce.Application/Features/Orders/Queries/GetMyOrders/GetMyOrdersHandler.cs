using AutoMapper.QueryableExtensions;
using ECommerce.Application.Features.Orders.DTOs;
using ECommerce.Application.Features.Orders.Queries.GetMyOrders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Queries.GetMyOrders
{
    public sealed class GetMyOrdersHandler
        : IRequestHandler<
            GetMyOrdersQuery,
            Result<PaginatedResult<OrderListItemDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetMyOrdersHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<PaginatedResult<OrderListItemDto>>> Handle(
            GetMyOrdersQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Orders
                .AsNoTracking()
                .Where(x => x.UserId == _currentUser.UserId)
                .OrderByDescending(x => x.CreatedAt);

            var totalCount = await query.CountAsync(cancellationToken);

            var orders = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<OrderListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<PaginatedResult<OrderListItemDto>>
                .Success(new PaginatedResult<OrderListItemDto>
                {
                    Items = orders,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize,
                    TotalCount = totalCount
                });
        }
    }
}
