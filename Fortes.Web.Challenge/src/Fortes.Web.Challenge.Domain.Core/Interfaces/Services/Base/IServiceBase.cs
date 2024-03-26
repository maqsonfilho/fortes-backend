using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Domain.Core.Interfaces.Services.Base;

public interface IServiceBase<TDto> where TDto : DtoBase
{
    Task<TDto> GetByIdAsync(Guid id);

    Task<IList<TDto>> GetAllAsync();

    Task<PagedResult<TDto>> GetAllPaginatedAsync(int pageNumber, int pageSize);

    Task<TDto> AddAsync(TDto obj);

    Task<TDto> UpdateAsync(TDto obj);

    Task RemoveAsync(Guid id);
}
