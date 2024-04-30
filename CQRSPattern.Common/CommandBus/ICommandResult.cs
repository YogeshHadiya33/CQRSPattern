namespace CQRSPattern.Common.CommandBus;

public interface ICommandResult
{
    string Message { get; set; }
    int StatusCode { get; set; }
    bool Success { get; set; }
    object Value { get; set; }
    List<string> Errors { get; set; }
}