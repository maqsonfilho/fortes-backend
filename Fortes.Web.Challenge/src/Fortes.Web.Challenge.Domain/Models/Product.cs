using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Domain.Models;

public class Product : ModelBase
{
    public Product() { }
    public Product(Guid id, DateTime createdAt) : base(id, createdAt) { }
    public string Code { get; set; }
    public string Description { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Value { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }
}
