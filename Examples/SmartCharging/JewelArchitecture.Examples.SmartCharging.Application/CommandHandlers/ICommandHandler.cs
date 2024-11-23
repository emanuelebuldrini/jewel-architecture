using JewelArchitecture.Examples.SmartCharging.Application.Commands;

namespace JewelArchitecture.Examples.SmartCharging.Application.CommandHandlers;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
