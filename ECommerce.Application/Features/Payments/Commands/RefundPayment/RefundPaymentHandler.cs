namespace ECommerce.Application.Features.Payments.Commands.RefundPayment;

public sealed class RefundPaymentHandler
    : IRequestHandler<RefundPaymentCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public RefundPaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        RefundPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _context.Payments.FindAsync(request.PaymentId, cancellationToken);

        if (payment == null)
        {
            return Result.Failure( new Error("Payment.NotFound", "Payment not found", ErrorType.NotFound));
        }

        payment.MarkAsRefunded();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}