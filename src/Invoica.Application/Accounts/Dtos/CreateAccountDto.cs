﻿using Invoica.Domain.Enums;

namespace Invoica.Application.Accounts.Dtos;

public class CreateAccountDto
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public AccountType Type { get; set; }

    public string? Iban { get; set; }

    public decimal CurrentBalance { get; set; }

    public bool IncludedInNetWorth { get; set; }

    public bool CanTransferFrom { get; set; }

    public bool CanTransferTo { get; set; }
}
