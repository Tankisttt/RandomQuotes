using System.Collections.Generic;

namespace RandomQuotes.Resources;

public interface IWriteResult<out T>
{
    bool IsSuccess { get; }
    T ResultData { get; }
    ErrorModel Error { get; }
}

public sealed class WriteResult<T> : IWriteResult<T>
{
    public WriteResult(T resultData)
    {
        ResultData = resultData;
        IsSuccess = true;
    }

    public WriteResult(ErrorModel error)
    {
        Error = error;
        IsSuccess = false;
    }

    public bool IsSuccess { get; }
    public T ResultData { get; }
    public ErrorModel Error { get; }

    public static WriteResult<T> FromValue(T value) => new(value);
    public static WriteResult<T> FromError(ErrorModel error) => new(error);
}

public class WriteResult
{
    public static WriteResult Successful => new();

    public WriteResult()
    {
        IsSuccess = true;
    }

    public WriteResult(ErrorModel error)
    {
        Error = error;
        IsSuccess = false;
    }

    public bool IsSuccess { get; }
    public ErrorModel Error { get; }

    public static WriteResult<T> FromValue<T>(T value) => new(value);
    public static WriteResult FromError(ErrorModel error) => new(error);
}

public class ErrorModel
{
    private ErrorModel(string key, string message, ErrorKind errorKind)
    {
        Key = key;
        Message = message;
        Kind = errorKind;
    }

    public string Key { get; }
    public string Message { get; }
    public ErrorKind Kind { get; }

    public IReadOnlyDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>(0);

    /// <summary>
    /// Generates model that indicates some generic error
    /// </summary>
    /// <param name="key">Error key</param>
    /// <param name="message">Error message</param>
    public static ErrorModel Default(string key, string message) => new(key, message, ErrorKind.Default);

    /// <summary>
    /// Generates model that indicates access or permission error 
    /// </summary>
    /// <param name="key">Error key</param>
    /// <param name="message">Error message</param>
    public static ErrorModel Forbidden(string key, string message) => new(key, message, ErrorKind.Forbidden);

    /// <summary>
    /// Generates model that indicates that resource was not found 
    /// </summary>
    /// <param name="key">Error key</param>
    /// <param name="message">Error message</param>
    public static ErrorModel NotFound(string key, string message) => new(key, message, ErrorKind.NotFound);

    /// <summary>
    /// Makes copy of the error model with new values. All parameters are options.
    /// </summary>
    /// <param name="key">New error key</param>
    /// <param name="message">New error message</param>
    /// <param name="errorKind">New error kind</param>
    /// <param name="errors">New validation result</param>
    /// <returns>Copy of error model</returns>
    public ErrorModel CopyWith(string key = null, string message = null,
        ErrorKind? errorKind = null,
        IReadOnlyDictionary<string, string[]> errors = null) =>
        new(key ?? Key, message ?? Message, errorKind ?? Kind)
        {
            Errors = errors ?? Errors
        };

    /// <summary>
    /// Helper method to convert 
    /// </summary>
    /// <returns></returns>
    public WriteResult ToWriteResult() => WriteResult.FromError(this);
}

public enum ErrorKind
{
    /// <summary>
    /// Generic error
    /// </summary>
    Default,

    /// <summary>
    /// Indicates access error
    /// </summary>
    Forbidden,

    /// <summary>
    /// Indicates resource was not found
    /// </summary>
    NotFound
}