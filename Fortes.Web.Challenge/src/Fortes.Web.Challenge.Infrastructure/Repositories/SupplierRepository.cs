using AutoMapper;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Models;
using Fortes.Web.Challenge.Domain.Models.Base;
using Fortes.Web.Challenge.Infrastructure.Data.Contexts;
using Fortes.Web.Challenge.Infrastructure.Repositories.Base;

namespace Fortes.Web.Challenge.Infrastructure.Repositories;

public class SupplierRepository : RepositoryBase<Supplier>, ISupplierRepository
{
    public SupplierRepository(FortesDbContext context, IMapper mapper) : base(context, mapper) { }
}
