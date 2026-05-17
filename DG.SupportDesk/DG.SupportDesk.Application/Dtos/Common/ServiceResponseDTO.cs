namespace DG.SupportDesk.Application.Dtos.Common;

public class ServiceResponseDTO<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
    public T? Data { get; set; }

    public static ServiceResponseDTO<T> SuccessResponse(T data, string message = "Success")
    {
        return new ServiceResponseDTO<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ServiceResponseDTO<T> ErrorResponse(string message)
    {
        return new ServiceResponseDTO<T>
        {
            Success = false,
            Message = message,
            Data = default
        };
    }
}