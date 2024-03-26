using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services.Base;

namespace Fortes.Web.Challenge.Domain.Core.Interfaces.Services;

public interface IOrderService : IServiceBase<OrderDto>
{
    Task<IList<OrderDto>> GetBySupplierIdAsync(Guid supplierId);
}
