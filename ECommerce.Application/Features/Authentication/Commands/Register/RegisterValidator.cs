using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Register
{
    public sealed class RegisterValidator
        : AbstractValidator<RegisterCommand>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
