

namespace Common.Domain.Generic.Utility.ResultPattern;

public enum ErrorType : ushort
{
    Unknown = 0,
    None = 1,
    ClassicalException = 2,
    DomainException = 3,
}
public record Error
{
    private Error(ErrorType type = ErrorType.None, string message = "")
    {
        Type = type; Message = message;
    }

    public static Error Create(ErrorType type, string message)
        => new Error(type, message);

    public static Error WithException<TException>(TException exception)
        where TException : Exception
        => new(type: ErrorType.ClassicalException, message: exception.ToString());
    public ErrorType Type { get; private init; } = ErrorType.Unknown;

    public string Message { get; private init; } = String.Empty;
}

public class Result<TResponse>
{
    public TResponse? Response { get; private init; } = default(TResponse?);

    public bool IsSuccess { get; private init; }

    public bool HasError { get; private init; } = false;

    public Error? Error { get; private init; } = default;

    public string? Message { get; private init; } = null;

    private Result(TResponse? response = default, bool isSuccess = true, bool hasError = false, Error? error = null,
        string? message = null)
    {
        this.Response = response;
        this.IsSuccess = isSuccess;
        this.HasError = hasError;
        this.Error = error;
        this.Message = message;
    }


    public Result<TResponse> Success(TResponse? response = default, string? message = null)
        => new Result<TResponse>(response: response, isSuccess: true, hasError: false, message: message);

    public Result<TResponse> Failure(TResponse? response = default, string? message = null, Error? error = default)
        => new Result<TResponse>(response: response, isSuccess: false, hasError: error is null, message: message, error: error);

    public Result<TResponse> WithError(Error? error = default)
        => new Result<TResponse>(error: error, hasError: true, isSuccess: false);

    public Result<TResponse> WithException(Exception e)
        => new Result<TResponse>(error: Error.WithException(e), hasError: true, isSuccess: false);

}