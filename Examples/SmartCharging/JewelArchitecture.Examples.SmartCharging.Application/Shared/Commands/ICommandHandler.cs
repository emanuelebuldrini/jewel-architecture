namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.Commands;

public interface ICommandHandler<TCommand>
    where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}
