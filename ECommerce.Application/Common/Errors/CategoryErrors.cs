using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Application.Common.Errors
{
    public static class CategoryErrors
    {
        public static readonly Error NotFound =
            new(
                "Category.NotFound",
                "The requested category was not found.",
                ErrorType.NotFound);

        public static readonly Error DuplicateName =
            new(
                "Category.DuplicateName",
                "A category with the same name already exists.",
                ErrorType.Conflict);

        public static readonly Error HasProducts =
            new(
                "Category.HasProducts",
                "Cannot delete a category that contains products.",
                ErrorType.Conflict);
    }
}
