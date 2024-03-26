using FluentValidation;
using Fortes.Web.Challenge.Api.Controllers.Base;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Application.Services;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fortes.Web.Challenge.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        /// <summary>
        /// Get all products with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A paginated list of products.</returns>
        [HttpGet("paged")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPaginated(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedResult = await _productService.GetAllPaginatedAsync(pageNumber, pageSize);
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all products.");
                return InternalServerErrorResponse("An error occurred while fetching all products.", ex);
            }
        }

        /// <summary>
        /// Get all products.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of products.</returns>
        [HttpGet]
        [Route("fetchAll")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pagedResult = await _productService.GetAllAsync();
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all products.");
                return InternalServerErrorResponse("An error occurred while fetching all products.", ex);
            }
        }

        /// <summary>
        /// Get a specific product by ID.
        /// </summary>
        /// <param name="id">The ID of the product.</param>
        /// <returns>The product with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFoundResponse("Product not found.");
                }
                return SuccessResponse(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching product with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while fetching product with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="request">The product object to be created.</param>
        /// <returns>The created product.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] ProductDto product)
        {
            try
            {
                if (product == null)
                {
                    return BadRequestResponse("Company object is null.");
                }

                var createdProduct = await _productService.AddAsync(product);
                return CustomSuccessResponse((int)HttpStatusCode.Created, createdProduct);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error while creating product.");
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating product.");
                return InternalServerErrorResponse("An error occurred while creating product.", ex);
            }
        }

        /// <summary>
        /// Remove a product by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var product = await _productService.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFoundResponse("Product not found.");
                }

                await _productService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing product with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while removing product with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Update an prouct.
        /// </summary>
        /// <param name="prouct">The prouct to be updated.</param>
        /// <returns>An updated prouct</returns>
        [HttpPut]
        [ProducesResponseType(typeof(ProductDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPaginated([FromBody] ProductDto order)
        {
            try
            {
                var updatedProduct = await _productService.UpdateAsync(order);
                return SuccessResponse(updatedProduct);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating prouct.");
                return InternalServerErrorResponse("An error occurred while updating order.", ex);
            }
        }
    }
}
