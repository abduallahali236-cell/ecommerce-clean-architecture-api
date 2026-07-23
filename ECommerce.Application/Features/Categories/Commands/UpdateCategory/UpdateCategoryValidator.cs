using ECommerce.Application.Features.Categories.Commands.UpdateCategory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryValidator
        : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(500);
        }
    }
}
