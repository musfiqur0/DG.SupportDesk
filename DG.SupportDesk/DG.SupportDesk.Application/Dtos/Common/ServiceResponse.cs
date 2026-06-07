namespace DG.SupportDesk.Application.Dtos.Common;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }

    public static ServiceResponse<T> SuccessResponse(T data, string message = "Success")
    {
        return new ServiceResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ServiceResponse<T> ErrorResponse(string message)
    {
        return new ServiceResponse<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }
}