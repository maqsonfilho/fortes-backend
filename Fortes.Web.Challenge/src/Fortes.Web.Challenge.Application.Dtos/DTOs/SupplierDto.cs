using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Application.Dtos.DTOs;

public class SupplierDto : DtoBase
{
    public string CorporateReason { get; set; }
    public string CompanyRegistrationNumber { get; set; }
    public string District { get; set; }
    public string ContactEmail { get; set; }
    public string ContactName { get; set; }
}
