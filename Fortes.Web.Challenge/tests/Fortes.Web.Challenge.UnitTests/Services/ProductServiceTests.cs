using AutoMapper;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.MappingProfiles;
using Fortes.Web.Challenge.Application.Services;
using Fortes.Web.Challenge.Application.Validators;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Repositories;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models.Base;
using Fortes.Web.Challenge.Domain.Models;
using Moq;

namespace Fortes.Web.Challenge.UnitTests.Services;

public class ProductServiceTests
{
    private readonly IProductService _sut;
    private readonly IMapper _mapper;
    private readonly CreateProductValidator _createProductValidator;

    private readonly Mock<IProductRepository> _productRepository;

    public ProductServiceTests()
    {
        _mapper = new MapperConfiguration(x =>
        {
            x.AddProfile(new ProductMappingProfile());
        }).CreateMapper();

        _productRepository = new Mock<IProductRepository>();
        _createProductValidator = new CreateProductValidator();
        _sut = new ProductService(_productRepository.Object, _mapper, _createProductValidator);
    }

    [Fact]
    public async Task AddAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var dto = new ProductDto();
        var model = new Product();

        _productRepository.Setup(r => r.AddAsync(It.IsAny<Product>())).ReturnsAsync(model);

        // Act
        var result = await _sut.AddAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsMappedDtoList()
    {
        // Arrange
        var models = new List<Product> { new Product(), new Product() };
        var dtos = new List<ProductDto> { new ProductDto(), new ProductDto() };

        _productRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(models);

        // Act
        var result = await _sut.GetAllAsync();
        
        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(models.Count, result.Count);
    }

    [Fact]
    public async Task GetAllPaginatedAsync_ReturnsPagedResultOfMappedDto()
    {
        // Arrange
        var pageNumber = 1;
        var pageSize = 10;
        var pagedResultModel = new PagedResult<Product> { Items = new List<Product> { new Product() }, TotalCount = 1, PageNumber = pageNumber, PageSize = pageSize };
        var pagedResultDto = new PagedResult<ProductDto> { Items = new List<ProductDto> { new ProductDto() }, TotalCount = 1, PageNumber = pageNumber, PageSize = pageSize };

        _productRepository.Setup(r => r.GetAllPaginatedAsync(pageNumber, pageSize)).ReturnsAsync(pagedResultModel);
        var expectedPagedMapped = new PagedResult<ProductDto>(pagedResultDto.Items, pagedResultDto.TotalCount, pagedResultDto.PageNumber, pagedResultDto.PageSize);

        // Act
        var result = await _sut.GetAllPaginatedAsync(pageNumber, pageSize);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Items);
        Assert.Equal(1, result.TotalCount);
    }

    [Fact]
    public async Task GetByIdAsync_ValidId_ReturnsMappedDto()
    {
        // Arrange
        var id = Guid.NewGuid();
        var model = new Product();
        var dto = new ProductDto();

        _productRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(model);

        // Act
        var result = await _sut.GetByIdAsync(id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
    }

    [Fact]
    public async Task RemoveAsync_ValidId_RemovesEntity()
    {
        // Arrange
        var id = Guid.NewGuid();
        var model = new Product();

        _productRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync(model);

        // Act
        await _sut.RemoveAsync(id);

        // Assert
        _productRepository.Verify(r => r.RemoveAsync(model), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _productRepository.Setup(r => r.GetByIdAsync(id)).ReturnsAsync((Product)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await _sut.RemoveAsync(id));
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var dto = new ProductDto();
        var model = new Product();

        _productRepository.Setup(r => r.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(model);

        // Act
        var result = await _sut.UpdateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
    }
}
