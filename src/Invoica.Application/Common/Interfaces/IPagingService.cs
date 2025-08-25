using Invoica.Application.Common.Models.Paging;

namespace Invoica.Application.Common.Interfaces;

public interface IPagingService
{
    Task<PagedResult<TDestination>> ToPagedResultAsync<TSource, TDestination>(
        IQueryable<TSource> query,
        PagedRequest paging,
        Func<TSource, TDestination>? converter = null,
        CancellationToken ct = default) where TSource : class;
}
