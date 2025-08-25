using Invoica.Application.Accounts.Dtos;
using Invoica.Application.Common.Models.Paging;

namespace Invoica.Application.Accounts.Interfaces;

public interface IAccountService
{
    Task<AccountResponseDto?> GetByIdAsync(int id, CancellationToken ct);

    Task<PagedResult<AccountResponseDto>> GetAllAsync(PagedRequest paging, CancellationToken ct);

    Task<int> CreateAsync(CreateAccountDto dto, CancellationToken ct);

    Task<bool> UpdateAsync(int id, UpdateAccountDto dto, CancellationToken ct);

    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
