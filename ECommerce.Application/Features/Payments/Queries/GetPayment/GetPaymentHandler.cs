
using ECommerce.Application.Common.Errors;
using ECommerce.Application.Features.Payments.DTOs;
namespace ECommerce.Application.Features.Payments.Queries.GetPayment;

public sealed class GetPaymentHandler
    : IRequestHandler<GetPaymentQuery, Result<PaymentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaymentHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Result<PaymentDto>> Handle(
        GetPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var payment = await _context.Payments
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == request.PaymentId,
                cancellationToken);

        if (payment is null)
            return Result<PaymentDto>.Failure(PaymentErrors.NotFound);

        return Result<PaymentDto>.Success(
            _mapper.Map<PaymentDto>(payment));
    }
}