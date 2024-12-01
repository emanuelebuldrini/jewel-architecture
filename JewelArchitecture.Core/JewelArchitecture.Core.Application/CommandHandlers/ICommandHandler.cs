using JewelArchitecture.Core.Application.Commands;

namespace JewelArchitecture.Core.Application.CommandHandlers;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
