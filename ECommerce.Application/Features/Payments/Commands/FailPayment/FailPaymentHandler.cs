namespace ECommerce.Application.Features.Payments.Commands.FailPayment;

public sealed class FailPaymentHandler
    : IRequestHandler<FailPaymentCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public FailPaymentHandler(
        IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(
        FailPaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _context.Payments.FindAsync(request.PaymentId, cancellationToken);

        if (payment == null)
        {
            return Result.Failure( new Error("Payment.NotFound", "Payment not found", ErrorType.NotFound));
        }

        payment.MarkAsFailed();

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}