using Invoica.Application.Accounts.Dtos;
using Invoica.Application.Accounts.Interfaces;
using Invoica.Application.Accounts.Mapping;
using Invoica.Application.Common.Interfaces;
using Invoica.Application.Common.Interfaces.Persistence;
using Invoica.Application.Common.Models.Paging;

namespace Invoica.Application.Accounts.Services;

public class AccountService(
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUser,
    IPagingService pagingService,
    AccountMapper mapper) : IAccountService
{
    public async Task<AccountResponseDto?> GetByIdAsync(int id, CancellationToken ct)
    {
        var account = await unitOfWork.Accounts.GetByIdAsync(id, currentUser.UserId, ct);
        return account is null ? null : mapper.ToDto(account);
    }

    public async Task<PagedResult<AccountResponseDto>> GetAllAsync(PagedRequest paging, CancellationToken ct)
    {
        var query = unitOfWork.Accounts.GetAllAsync(currentUser.UserId);
        return await pagingService.ToPagedResultAsync(query, paging, mapper.ToDto, ct);
    }

    public async Task<int> CreateAsync(CreateAccountDto dto, CancellationToken ct)
    {
        var entity = mapper.ToEntity(dto);
        await unitOfWork.Accounts.AddAsync(entity, ct);
        await unitOfWork.SaveChangesAsync(ct);

        return entity.Id;
    }

    public async Task<bool> UpdateAsync(int id, UpdateAccountDto dto, CancellationToken ct)
    {
        var account = await unitOfWork.Accounts.FindEntityByIdAsync(id, currentUser.UserId, ct);
        if (account is null)
            return false;

        mapper.UpdateEntity(dto, account);

        await unitOfWork.Accounts.UpdateAsync(account, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {
        var account = await unitOfWork.Accounts.FindEntityByIdAsync(id, currentUser.UserId, ct);
        if (account is null)
            return false;

        await unitOfWork.Accounts.DeleteAsync(account, ct);
        await unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}

