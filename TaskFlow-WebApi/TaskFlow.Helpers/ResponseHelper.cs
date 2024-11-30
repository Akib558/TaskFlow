using System;

namespace TaskFlow.Helpers;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public T Errors { get; set; }

    public static ApiResponse<T> SuccessResponse(T data, string message)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            Data = data
        };
    }
    public static ApiResponse<T> ErrorResponse(T errors, string message)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            Errors = errors
        };
    }

}
