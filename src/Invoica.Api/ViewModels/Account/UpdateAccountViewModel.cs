﻿using Invoica.Domain.Enums;

namespace Invoica.Api.ViewModels.Account;

public class UpdateAccountViewModel
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public AccountType Type { get; set; }

    public string? Iban { get; set; }

    public bool IncludedInNetWorth { get; set; }

    public bool CanTransferFrom { get; set; }

    public bool CanTransferTo { get; set; }
}
