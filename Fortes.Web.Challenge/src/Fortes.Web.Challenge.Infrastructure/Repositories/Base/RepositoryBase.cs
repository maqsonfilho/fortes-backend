using AutoMapper;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories.Base;
using Fortes.Web.Challenge.Domain.Models.Base;
using Fortes.Web.Challenge.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Infrastructure.Repositories.Base;

public class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : ModelBase
{
    protected readonly FortesDbContext _context;
    protected readonly IMapper _mapper;

    public RepositoryBase(FortesDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<TModel> AddAsync(TModel obj)
    {
        _context.Set<TModel>().Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task<IList<TModel>> GetAllAsync()
    {
        var query = _context.Set<TModel>();
        _context.SaveChanges();

        return await query.ToListAsync();
    }

    public virtual async Task<PagedResult<TModel>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _context.Set<TModel>().CountAsync();
        var items = await _context.Set<TModel>().Skip((pageNumber - 1) * pageSize)
                                .Take(pageSize)
                                .ToListAsync();

        return new PagedResult<TModel>(items, totalCount, pageNumber, pageSize);
    }

    public virtual async Task<TModel> GetByIdAsync(Guid id)
    {
        var query = _context.Set<TModel>();
        var result = await query.FindAsync(id);

        if (result == null)
        {
            throw new Exception("Error while find model");
        }

        return result;
    }

    public async Task<TModel> UpdateAsync(TModel obj)
    {
        _context.Update(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task RemoveAsync(TModel obj)
    {
        var query = _context.Set<TModel>();
        query.Remove(obj);
        await _context.SaveChangesAsync();
    }
}
