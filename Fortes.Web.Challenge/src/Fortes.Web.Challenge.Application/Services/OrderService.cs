using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.Services.Base;
using Fortes.Web.Challenge.Application.Validators;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models;

namespace Fortes.Web.Challenge.Application.Services;

public class OrderService : ServiceBase<OrderDto, Order, CreateOrderValidator>, IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductService _productService;

    public OrderService(IOrderRepository orderRepository, IMapper mapper, CreateOrderValidator validator,
        IProductService productService)
        : base (orderRepository, mapper, validator)
    {
        _orderRepository = orderRepository;
        _productService = productService;
    }


    public override async Task<OrderDto> AddAsync(OrderDto obj)
    {
        var prod = await _productService.GetByIdAsync(obj.ProductId);

        if (prod == null)
        {
            throw new Exception($"Product {obj.ProductId} invalid");
        }

        obj.TotalValue = prod.Value * obj.Amount;
        obj.Date = DateTime.UtcNow;

        return await base.AddAsync(obj);
    }


    public override async Task<OrderDto> UpdateAsync(OrderDto obj)
    {
        var prod = await _productService.GetByIdAsync(obj.ProductId);

        if (prod == null)
        {
            throw new Exception($"Product {obj.ProductId} invalid");
        }

        obj.TotalValue = prod.Value * obj.Amount;
        obj.Date = DateTime.UtcNow;

        return await base.UpdateAsync(obj);
    }


    public async Task<IList<OrderDto>> GetBySupplierIdAsync(Guid supplierId)
    {
        var resultList = await _orderRepository.GetBySupplierIdAsync(supplierId);
        return _mapper.Map<IList<OrderDto>>(resultList);
    }
}

