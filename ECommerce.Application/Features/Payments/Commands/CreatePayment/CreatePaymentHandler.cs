namespace ECommerce.Application.Features.Payments.Commands.CreatePayment;

public sealed class CreatePaymentHandler
    : IRequestHandler<CreatePaymentCommand, Result<int>>
{
    private readonly IApplicationDbContext _context;

    public CreatePaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<int>> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {

        var Payment = new Payment(
            request.OrderId,
            request.Amount,
            request.PaymentMethod
            );

        _context.Payments.Add(Payment);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<int>.Success(Payment.Id);

    }
}