namespace Fortes.Web.Challenge.Application.Dtos.Responses.Base;

public class GenericResponseDto
{
    public bool IsSuccess { get; set; }

    public GenericResponseDto(bool isSucess)
    {
        IsSuccess = isSucess;
    }
}
