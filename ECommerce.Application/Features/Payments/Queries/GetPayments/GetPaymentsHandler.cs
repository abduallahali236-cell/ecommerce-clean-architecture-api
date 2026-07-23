using AutoMapper.QueryableExtensions;
using ECommerce.Application.Features.Payments.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Payments.Queries.GetPayments
{
    public sealed class GetPaymentsHandler
        : IRequestHandler<GetPaymentsQuery,
            Result<PaginatedResult<PaymentListItemDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetPaymentsHandler(
            IApplicationDbContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedResult<PaymentListItemDto>>> Handle(
            GetPaymentsQuery request,
            CancellationToken cancellationToken)
        {
            var query = _context.Payments
                .AsNoTracking()
                .OrderBy(x => x.Amount);

            var totalCount = await query.CountAsync(cancellationToken);

            var payments = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<PaymentListItemDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            var result = new PaginatedResult<PaymentListItemDto>
            {
                Items = payments,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };

            return Result<PaginatedResult<PaymentListItemDto>>
                .Success(result);
        }
    }
}