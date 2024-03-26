using FluentValidation;
using Fortes.Web.Challenge.Application.Dtos.DTOs;

namespace Fortes.Web.Challenge.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        public CreateProductValidator()
        {
            
        }
    }
}
