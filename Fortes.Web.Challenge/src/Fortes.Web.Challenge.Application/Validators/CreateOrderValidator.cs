using FluentValidation;
using Fortes.Web.Challenge.Application.Dtos.DTOs;

namespace Fortes.Web.Challenge.Application.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator()
        {
            
        }
    }
}
