namespace Fortes.Web.Challenge.Application.Dtos.Responses.Base;

public class SuccessResponseDto : GenericResponseDto
{
    public object Data { get; set; }

    public SuccessResponseDto(object data) : base(true)
    {
        Data = data;
    }
}
