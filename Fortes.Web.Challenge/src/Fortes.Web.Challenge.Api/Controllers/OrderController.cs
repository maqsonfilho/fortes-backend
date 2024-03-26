using FluentValidation;
using Fortes.Web.Challenge.Api.Controllers.Base;
using Fortes.Web.Challenge.Application.Dtos.DTOs;
using Fortes.Web.Challenge.Domain.Core.Interfaces.Services;
using Fortes.Web.Challenge.Domain.Models;
using Fortes.Web.Challenge.Domain.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fortes.Web.Challenge.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderService _orderService;

        public OrderController(ILogger<OrderController> logger, IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        /// <summary>
        /// Get all orders with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A paginated list of orders.</returns>
        [HttpGet]
        [Route("paged")]
        [ProducesResponseType(typeof(PagedResult<OrderDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPaginated(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedResult = await _orderService.GetAllPaginatedAsync(pageNumber, pageSize);
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all orders.");
                return InternalServerErrorResponse("An error occurred while fetching all orders.", ex);
            }
        }

        /// <summary>
        /// Get all orders.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of orders.</returns>
        [HttpGet]
        [Route("fetchAll")]
        [ProducesResponseType(typeof(PagedResult<ProductDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var pagedResult = await _orderService.GetAllAsync();
                return SuccessResponse(pagedResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while fetching all orders.");
                return InternalServerErrorResponse("An error occurred while fetching all orders.", ex);
            }
        }

        /// <summary>
        /// Get a specific order by ID.
        /// </summary>
        /// <param name="id">The ID of the order.</param>
        /// <returns>The order with the specified ID.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                {
                    return NotFoundResponse("Order not found.");
                }
                return SuccessResponse(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching order with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while fetching order with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Get a specific order by ID.
        /// </summary>
        /// <param name="supplierId">The ID of the order.</param>
        /// <returns>The order with the specified ID.</returns>
        [HttpGet("supplier")]
        [ProducesResponseType(typeof(OrderDto), 200)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetBySupplierId([FromQuery] Guid supplierId)
        {
            try
            {
                if (supplierId == Guid.Empty)
                {
                    return BadRequestResponse("SupplierId is mandatory");
                }

                var order = await _orderService.GetBySupplierIdAsync(supplierId);
                if (order == null)
                {
                    return NotFoundResponse("Order not found.");
                }
                return SuccessResponse(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while fetching orders from supplier ID {supplierId}.");
                return InternalServerErrorResponse($"An error occurred while fetching orders from supplier ID {supplierId}.", ex);
            }
        }

        /// <summary>
        /// Create a new order.
        /// </summary>
        /// <param name="request">The order object to be created.</param>
        /// <returns>The created order.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] OrderDto order)
        {
            try
            {
                if (order == null)
                {
                    return BadRequestResponse("Order object is null.");
                }

                var createdOrder = await _orderService.AddAsync(order);
                return CustomSuccessResponse((int)HttpStatusCode.Created, createdOrder);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Error while creating order.");
                return HandleValidationException(ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating order.");
                return InternalServerErrorResponse("An error occurred while creating order.", ex);
            }
        }

        /// <summary>
        /// Remove a order by ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Remove(Guid id)
        {
            try
            {
                var order = await _orderService.GetByIdAsync(id);
                if (order == null)
                {
                    return NotFoundResponse("Order not found.");
                }

                await _orderService.RemoveAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing order with ID {id}.");
                return InternalServerErrorResponse($"An error occurred while removing order with ID {id}.", ex);
            }
        }

        /// <summary>
        /// Update an order.
        /// </summary>
        /// <param name="order">The order to be updated.</param>
        /// <returns>An updated order</returns>
        [HttpPut]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] OrderDto order)
        {
            try
            {
                var updatedOrder = await _orderService.UpdateAsync(order);
                return SuccessResponse(updatedOrder);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating order.");
                return InternalServerErrorResponse("An error occurred while updating order.", ex);
            }
        }
    }
}

