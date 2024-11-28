namespace JewelArchitecture.Core.Application.Commands;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
