using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Common.Factory;

public class CommandResultFactory : ICommandResultFactory
{
    public CommandResult Create(bool success, string message, object value, int statusCode)
    {
        return new CommandResult { Success = success, Message = message, Value = value, StatusCode = statusCode };
    }

    public CommandResult Create(bool success, string message, int statusCode)
    {
        return new CommandResult { Success = success, Message = message, Value = null, StatusCode = statusCode };
    }

    public CommandResult Create(bool success, int statusCode)
    {
        return new CommandResult { Success = success, Message = string.Empty, Value = null, StatusCode = statusCode };
    }

    public CommandResult Create(bool success, string message)
    {
        return new CommandResult { Success = success, Message = message };
    }

    public CommandResult Create(bool success, int statusCode, object value)
    {
        return new CommandResult { Success = success, Message = string.Empty, Value = value, StatusCode = statusCode };
    }

    public CommandResult Create(bool success, int statusCode, List<string> error)
    {
        return new CommandResult { Success = success, Errors = error, StatusCode = statusCode };
    }
}