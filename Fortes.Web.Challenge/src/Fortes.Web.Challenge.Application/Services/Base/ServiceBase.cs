using AutoMapper;
using FluentValidation;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories.Base;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services.Base;
using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Application.Services.Base;

public class ServiceBase<TDto, TModel, TValidator> : IServiceBase<TDto>
    where TDto : DtoBase
    where TModel : ModelBase
    where TValidator : AbstractValidator<TDto>
{
    protected readonly IRepositoryBase<TModel> _repository;
    protected readonly IMapper _mapper;
    protected readonly TValidator _validator;

    public ServiceBase(IRepositoryBase<TModel> repository, IMapper mapper, TValidator validator)
    {
        _repository = repository;
        _mapper = mapper;
        _validator = validator;
    }

    public virtual async Task<TDto> AddAsync(TDto obj)
    {
        var validationResult = await _validator.ValidateAsync(obj);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        TModel entity = _mapper.Map<TModel>(obj);
        entity = await _repository.AddAsync(entity);

        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<IList<TDto>> GetAllAsync()
    {
        var entities = await _repository.GetAllAsync();

        return _mapper.Map<IList<TDto>>(entities);
    }

    public virtual async Task<PagedResult<TDto>> GetAllPaginatedAsync(int pageNumber, int pageSize)
    {
        var pagedResult = await _repository.GetAllPaginatedAsync(pageNumber, pageSize);
        var mappedItems = _mapper.Map<IList<TDto>>(pagedResult.Items);
        var pagedMapped = new PagedResult<TDto>(mappedItems, pagedResult.TotalCount, pagedResult.PageNumber, pagedResult.PageSize);

        return _mapper.Map<PagedResult<TDto>>(pagedMapped);
    }

    public virtual async Task<TDto> GetByIdAsync(Guid id)
    {
        var entity = await _repository.GetByIdAsync(id);
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task RemoveAsync(Guid id)
    {
        var entityToBeRemoved = await _repository.GetByIdAsync(id);

        if (entityToBeRemoved == null)
            throw new Exception($"Cannot remove entity with Id {id}. Entity not found.");

        await _repository.RemoveAsync(entityToBeRemoved);
    }

    public virtual async Task<TDto> UpdateAsync(TDto obj)
    {
        var entity = _mapper.Map<TModel>(obj);
        entity = await _repository.UpdateAsync(entity);

        return _mapper.Map<TDto>(entity);
    }
}
