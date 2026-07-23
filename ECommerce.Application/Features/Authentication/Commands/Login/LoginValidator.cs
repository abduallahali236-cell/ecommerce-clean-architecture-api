using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Authentication.Commands.Login
{
    public sealed class LoginValidator
        : AbstractValidator<LoginCommand>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
