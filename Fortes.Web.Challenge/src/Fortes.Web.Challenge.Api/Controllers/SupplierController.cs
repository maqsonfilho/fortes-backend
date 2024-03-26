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
    public class SupplierController : BaseController
    {
        private readonly ILogger<SupplierController> _logger;
        private readonly ISupplierService _supplierService;

        public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
        {
            _logger = logger;
            _supplierService = supplierService;
        }

        /// <summary>
        /// Get all suppliers with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A paginated list of suppliers.</returns>
        [HttpGet]
        [Route("paged")]
        [ProducesResponseType(typeof(PagedResult<SupplierDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPaginated(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedResult = await _supplierService.GetAllPaginatedAsync(pageNumber, pageSize);
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all suppliers.");
                return InternalServerErrorResponse("An error occurred while fetching all suppliers.", ex);
            }
        }

        /// <summary>
        /// Get all suppliers.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of suppliers.</returns>
        [HttpGet]
        [Route("fetchAll")]
        [ProducesResponseType(typeof(PagedResult<SupplierDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pagedResult = await _supplierService.GetAllAsync();
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all suppliers.");
                return InternalServerErrorResponse("An error occurred while fetching all suppliers.", ex);
            }
        }

        /// <summary>
        /// Get a specific supplier by ID.
        /// </summary>
        /// <param name="id">The ID of the supplier.</param>
        /// <returns>The supplier with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SupplierDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                if (supplier == null)
                {
                    return NotFoundResponse("Supplier not found.");
                }
                return SuccessResponse(supplier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching supplier with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while fetching supplier with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Create a new supplier.
        /// </summary>
        /// <param name="request">The supplier object to be created.</param>
        /// <returns>The created supplier.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(SupplierDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] SupplierDto supplier)
        {
            try
            {
                if (supplier == null)
                {
                    return BadRequestResponse("Supplier object is null.");
                }

                var createdSupplier = await _supplierService.AddAsync(supplier);
                return CustomSuccessResponse((int)HttpStatusCode.Created, createdSupplier);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error while creating supplier.");
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating supplier.");
                return InternalServerErrorResponse("An error occurred while creating supplier.", ex);
            }
        }

        /// <summary>
        /// Remove a supplier by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var supplier = await _supplierService.GetByIdAsync(id);
                if (supplier == null)
                {
                    return NotFoundResponse("Supplier not found.");
                }

                await _supplierService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing supplier with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while removing supplier with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Update an supplier.
        /// </summary>
        /// <param name="supplier">The supplier to be updated.</param>
        /// <returns>An updated supplier</returns>
        [HttpPut]
        [ProducesResponseType(typeof(PagedResult<SupplierDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] SupplierDto supplier)
        {
            try
            {
                var updatedSupplier = await _supplierService.UpdateAsync(supplier);
                return SuccessResponse(updatedSupplier);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating supplier.");
                return InternalServerErrorResponse("An error occurred while updating supplier.", ex);
            }
        }
    }
}
