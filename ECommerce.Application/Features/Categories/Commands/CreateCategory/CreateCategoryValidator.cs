using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.CreateCategory
{
    public sealed class UpdateCategoryValidator
        : AbstractValidator<CreateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
