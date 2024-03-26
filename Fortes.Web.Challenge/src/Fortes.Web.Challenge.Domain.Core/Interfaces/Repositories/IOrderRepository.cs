using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories.Base;
using Fortes.Web.Challenge.Domain.Models;

namespace Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
    Task<IList<Order>> GetBySupplierIdAsync(Guid supplierId);
}
