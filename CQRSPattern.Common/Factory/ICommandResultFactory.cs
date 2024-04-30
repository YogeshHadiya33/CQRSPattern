using CQRSPattern.Common.CommandBus;

namespace CQRSPattern.Common.Factory;

public interface ICommandResultFactory
{
    CommandResult Create(bool success, int statusCode);
    CommandResult Create(bool success, string message);
    CommandResult Create(bool success, string message, int statusCode);
    CommandResult Create(bool success, string message, object value, int statusCode);
    CommandResult Create(bool success, int statusCode, object value);
    CommandResult Create(bool success, int statusCode, List<string> error);
}