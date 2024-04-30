using Microsoft.AspNetCore.Http;

namespace CQRSPattern.Common.CommandBus;

public class CommandResult : ICommandResult
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public object Value { get; set; }
    public List<string> Errors { get; set; }
    public int StatusCode { get; set; } = StatusCodes.Status200OK;
}