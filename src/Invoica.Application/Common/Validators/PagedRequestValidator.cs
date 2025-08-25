using FluentValidation;
using Invoica.Application.Common.Models.Paging;

namespace Invoica.Application.Common.Validators;

// ReSharper disable once UnusedType.Global
public class PagedRequestValidator : AbstractValidator<PagedRequest>
{
    public PagedRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .When(x => x.PageNumber.HasValue);

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100)
            .When(x => x.PageSize.HasValue);
    }
}
