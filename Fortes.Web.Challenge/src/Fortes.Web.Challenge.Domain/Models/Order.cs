using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Domain.Models;

public class Order : ModelBase
{
    public Order() { }
    public Order(Guid id, DateTime createdAt) : base(id, createdAt) { }

    public string Code { get; set; }
    public DateTime Date { get; set; }
    public int Amount { get; set; }
    public decimal TotalValue { get; set; }

    public Guid ProductId { get; set; }
    public Guid SupplierId { get; set; }
    public virtual Product Product { get; set; }
    public virtual Supplier Supplier { get; set; }
}
