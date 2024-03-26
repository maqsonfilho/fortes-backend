using Fortes.Web.Challenge.Domain.Models.Base;

namespace Fortes.Web.Challenge.Application.Dtos.DTOs;

public class ProductDto : DtoBase
{
    public string Code { get; set; }
    public string Description { get; set; }
    public DateTime RegistrationDate { get; set; }
    public decimal Value { get; set; }
}
