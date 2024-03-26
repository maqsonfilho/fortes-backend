using AutoMapper;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Models;
using Fortes.Web.Challenge.Infrastructure.Data.Contexts;
using Fortes.Web.Challenge.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fortes.Web.Challenge.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
   public OrderRepository(FortesDbContext context, IMapper mapper) : base(context, mapper) { }

    public async Task<IList<Order>> GetBySupplierIdAsync(Guid supplierId)
    {
        return await _context.Orders
            .Include(o => o.Supplier)
            .Include(o => o.Product)
            .Where(o => o.SupplierId == supplierId).ToListAsync();
    }
}
