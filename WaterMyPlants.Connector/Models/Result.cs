namespace WaterMyPlants.Connector.Models;

public class Result<T> where T : notnull
{
    public T? Value { get; }
    public string? ErrorMessage { get; }
    public bool IsSuccess => ErrorType == ErrorTypes.None;
    public ErrorTypes ErrorType { get; }

    private Result(T? value, string? errorMessage)
    {
        Value = value;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T value) => new Result<T>(value, null);
    public static Result<T> Failure(string errorMessage) => new Result<T>(default, errorMessage);
}
