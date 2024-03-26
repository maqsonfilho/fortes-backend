namespace Fortes.Web.Challenge.Application.Dtos.Responses.Base;

public class ErrorResponseDto : GenericResponseDto
{
    public string Message { get; set; }
    public string? Exception { get; set; }

    public ErrorResponseDto(string message) : base(false)
    {
        Message = message;
        Exception = string.Empty;
    }


    public ErrorResponseDto(string message, string exception) : base(false)
    {
        Message = message;
        Exception = exception;
    }
}
