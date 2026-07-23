using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Orders.Queries.GetOrderById
{
    public sealed class GetOrderByIdHandler
        : IRequestHandler<GetOrderByIdQuery, Result<OrderDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public GetOrderByIdHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<Result<OrderDto>> Handle(
            GetOrderByIdQuery request,
            CancellationToken cancellationToken)
        {
            var order = await _context.Orders
                .AsNoTracking()
                .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(
                    x => x.Id == request.OrderId &&
                         x.UserId == _currentUser.UserId,
                    cancellationToken);

            if (order is null)
                return Result<OrderDto>.Failure(OrderErrors.NotFound);

            return Result<OrderDto>.Success(
                _mapper.Map<OrderDto>(order));
        }
    }
}
