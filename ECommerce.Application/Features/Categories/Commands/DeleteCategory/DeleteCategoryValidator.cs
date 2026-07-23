using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryValidator
        : AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0);
        }
    }
}
