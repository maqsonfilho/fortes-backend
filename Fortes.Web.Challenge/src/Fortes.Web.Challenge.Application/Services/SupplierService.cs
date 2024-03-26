using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.Services.Base;
using Fortes.Web.Challenge.Application.Validators;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models;

namespace Fortes.Web.Challenge.Application.Services;

public class SupplierService : ServiceBase<SupplierDto, Supplier, CreateSupplierValidator>, ISupplierService
{
    public SupplierService(ISupplierRepository orderRepository, IMapper mapper, CreateSupplierValidator validator)
        : base(orderRepository, mapper, validator)
    { }
}
