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

public class SupplierServiceTests
{
    private readonly ISupplierService _sut;
    private readonly IMapper _mapper;
    private readonly CreateSupplierValidator _createSupplierValidator;

    private readonly Mock<ISupplierRepository> _supplierValidator;

    public SupplierServiceTests()
    {
        _mapper = new MapperConfiguration(x =>
        {
            x.AddProfile(new SupplierMappingProfile());
        }).CreateMapper();

        _supplierValidator = new Mock<ISupplierRepository>();
        _createSupplierValidator = new CreateSupplierValidator();

        _sut = new SupplierService(_supplierValidator.Object, _mapper, _createSupplierValidator);
    }

    [Fact]
    public async Task AddAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var dto = new SupplierDto();
        var model = new Supplier();
        var validationResult = new FluentValidation.Results.ValidationResult();

        _supplierValidator.Setup(r => r.AddAsync(It.IsAny<Supplier>())).ReturnsAsync(model);

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
        var models = new List<Supplier> { new Supplier(), new Supplier() };
        var dtos = new List<SupplierDto> { new SupplierDto(), new SupplierDto() };

        _supplierValidator.Setup(r => r.GetAllAsync()).ReturnsAsync(models);

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
        var pagedResultModel = new PagedResult<Supplier> { Items = new List<Supplier> { new Supplier() }, TotalCount = 1, PageNumber = pageNumber, PageSize = pageSize };
        var pagedResultDto = new PagedResult<SupplierDto> { Items = new List<SupplierDto> { new SupplierDto() }, TotalCount = 1, PageNumber = pageNumber, PageSize = pageSize };

        _supplierValidator.Setup(r => r.GetAllPaginatedAsync(pageNumber, pageSize)).ReturnsAsync(pagedResultModel);
        var expectedPagedMapped = new PagedResult<SupplierDto>(pagedResultDto.Items, pagedResultDto.TotalCount, pagedResultDto.PageNumber, pagedResultDto.PageSize);

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
        var model = new Supplier();
        var dto = new SupplierDto();

        _supplierValidator.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(model);

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
        var model = new Supplier();

        _supplierValidator.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(model);

        // Act
        await _sut.RemoveAsync(id);

        // Assert
        _supplierValidator.Verify(r => r.RemoveAsync(model), Times.Once);
    }

    [Fact]
    public async Task RemoveAsync_InvalidId_ThrowsException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _supplierValidator.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Supplier)null);

        // Act and Assert
        await Assert.ThrowsAsync<Exception>(async () => await _sut.RemoveAsync(id));
    }

    [Fact]
    public async Task UpdateAsync_ValidDto_ReturnsMappedDto()
    {
        // Arrange
        var dto = new SupplierDto();
        var model = new Supplier();

        _supplierValidator.Setup(r => r.UpdateAsync(It.IsAny<Supplier>())).ReturnsAsync(model);

        // Act
        var result = await _sut.UpdateAsync(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(model.Id, result.Id);
    }
}
