using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Domain.Models;

public class Supplier : ModelBase
{
    public Supplier() { }
    public Supplier(Guid id, DateTime createdAt) : base(id, createdAt) { }

    public string CorporateReason { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string District { get; set; }
    public string ContactEmail { get; set; }
    public string ContactName { get; set; }
    public virtual IEnumerable<Order> Orders { get; set; }

}
