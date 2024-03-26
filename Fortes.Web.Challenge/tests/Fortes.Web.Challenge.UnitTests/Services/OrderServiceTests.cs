using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.MappingProfiles;
using Fortes.Web.Challenge.Application.Services;
using Fortes.Web.Challenge.Application.Validators;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models;
using Moq;

namespace Fortes.Web.Challenge.UnitTests.Services;

public class OrderServiceTests
{
    private readonly IOrderService _sut;
    private readonly IMapper _mapper;
    private readonly CreateOrderValidator _createOrderValidator;

    private readonly Mock<IOrderRepository> _orderRepositoryMock;
    private readonly Mock<IProductService> _productServiceMock;

    public OrderServiceTests()
    {
        _mapper = new MapperConfiguration(x =>
        {
            x.AddProfile(new OrderMappingProfile());
        }).CreateMapper();

        _createOrderValidator = new CreateOrderValidator();
        _orderRepositoryMock = new Mock<IOrderRepository>();
        _productServiceMock = new Mock<IProductService>();
        _sut = new OrderService(_orderRepositoryMock.Object, _mapper, _createOrderValidator, _productServiceMock.Object);
    }

    [Fact]
    public async Task AddAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var orderDto = new OrderDto()
        {
            Amount = 10,
            Code = "XPTOABC",
            ProductId = Guid.NewGuid(),
            SupplierId = Guid.NewGuid(),
        };
        var productDto = new ProductDto()
        {
            Id = orderDto.ProductId,
            Value = 3.99M
        };
        var expectedValue = productDto.Value * orderDto.Amount;
        var orderModel = new Order(Guid.NewGuid(), DateTime.UtcNow)
        {
            Amount = 10,
            Code = "XPTOABC",
            ProductId = Guid.NewGuid(),
            SupplierId = Guid.NewGuid(),
            TotalValue = expectedValue,
        };
        _productServiceMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(productDto);
        _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>())).ReturnsAsync(orderModel);

        // Act
        var result = await _sut.AddAsync(orderDto);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.Equal(orderModel.Id, result.Id);
        Assert.Equal(expectedValue, result.TotalValue);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtoList()
    {
        // Arrange
        var orderModels = new List<Order> { new Order(), new Order() };
        var orderDtos = new List<OrderDto> { new OrderDto(), new OrderDto() };

        _orderRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(orderModels);

        // Act
        var result = await _sut.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(orderModels.Count, result.Count);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsMappedDto()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderModel = new Order(orderId, DateTime.UtcNow);
        var orderDto = new OrderDto();

        _orderRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(orderModel);

        // Act
        var result = await _sut.GetByIdAsync(orderId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderModel.Id, result.Id);
    }

    [Fact]
    public async Task RemoveAsync_ValidId_RemovesEntity()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderModel = new Order();

        _orderRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(orderModel);

        // Act
        await _sut.RemoveAsync(orderId);

        // Assert
        _orderRepositoryMock.Verify(r => r.RemoveAsync(orderModel), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var orderDto = new OrderDto();
        var orderModel = new Order();

        _orderRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Order>())).ReturnsAsync(orderModel);

        // Act
        var result = await _sut.UpdateAsync(orderDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(orderModel.Id, result.Id);
    }
}
