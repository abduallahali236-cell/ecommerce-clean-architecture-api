namespace ECommerce.Application.Common.Errors;

public static class PaymentErrors
{
    public static readonly Error NotFound =
        new(
            "Payment.NotFound",
            "Payment was not found.",
            ErrorType.NotFound);

    public static readonly Error AlreadyProcessed =
        new(
            "Payment.AlreadyProcessed",
            "Payment has already been processed.",
            ErrorType.Conflict);

    public static readonly Error InvalidOrder =
        new(
            "Payment.InvalidOrder",
            "Order was not found.",
            ErrorType.NotFound);

    public static readonly Error AlreadyExists =
        new(
            "Payment.AlreadyExists",
            "A payment already exists for this order.",
            ErrorType.Conflict);
}