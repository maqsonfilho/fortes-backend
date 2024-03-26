using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Application.Dtos.DTOs;

public class OrderDto : DtoBase
{
    public string Code { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public decimal TotalValue { get; set; }
    public Guid ProductId { get; set; }
    public Guid SupplierId { get; set; }
    public ProductDto Product { get; set; }
    public SupplierDto Supplier { get; set; }
}
