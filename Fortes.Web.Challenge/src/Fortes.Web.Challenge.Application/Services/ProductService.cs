using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.Services.Base;
using Fortes.Web.Challenge.Application.Validators;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models;

namespace Fortes.Web.Challenge.Application.Services;

public class ProductService : ServiceBase<ProductDto, Product, CreateProductValidator>, IProductService
{
    public ProductService(IProductRepository orderRepository, IMapper mapper, CreateProductValidator validator)
        : base(orderRepository, mapper, validator)
    { }

    public override Task<ProductDto> AddAsync(ProductDto obj)
    {
        obj.RegistrationDate = DateTime.UtcNow;
        return base.AddAsync(obj);
    }
}
