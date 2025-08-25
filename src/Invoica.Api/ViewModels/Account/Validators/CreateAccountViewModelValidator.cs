﻿using FluentValidation;

namespace Invoica.Api.ViewModels.Account.Validators;

public class CreateAccountViewModelValidator : AbstractValidator<CreateAccountViewModel>
{
    public CreateAccountViewModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Account name is required.")
            .MaximumLength(255);

        RuleFor(x => x.Type)
            .IsInEnum()
            .WithMessage("Account type is required.");

        RuleFor(x => x.Description)
            .MaximumLength(255);
    }
}
