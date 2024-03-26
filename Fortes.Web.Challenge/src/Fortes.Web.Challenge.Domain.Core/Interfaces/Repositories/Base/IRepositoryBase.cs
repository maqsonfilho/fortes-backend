using Fortes.Web.Challenge.Domain.Models.Base;
using System.Diagnostics.CodeAnalysis;

namespace Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories.Base;

public interface IRepositoryBase<TModel> where TModel : ModelBase
{
    Task<TModel> GetByIdAsync(Guid id);

    Task<IList<TModel>> GetAllAsync();

    Task<PagedResult<TModel>> GetAllPaginatedAsync(int pageNumber, int pageSize);

    Task<TModel> AddAsync(TModel obj);

    Task<TModel> UpdateAsync(TModel obj);

    Task RemoveAsync(TModel obj);
}
