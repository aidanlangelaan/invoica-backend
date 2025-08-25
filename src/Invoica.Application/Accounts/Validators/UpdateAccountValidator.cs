using Invoica.Application.Accounts.Dtos;
using FluentValidation;

namespace Invoica.Application.Accounts.Validators;

// ReSharper disable once UnusedType.Global
public class UpdateAccountValidator : AbstractValidator<UpdateAccountDto>
{
    public UpdateAccountValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Type).IsInEnum();
    }
}
