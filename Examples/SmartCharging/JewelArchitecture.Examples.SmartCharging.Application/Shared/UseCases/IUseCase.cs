namespace JewelArchitecture.Examples.SmartCharging.Application.Shared.UseCases;

public interface IUseCase<TInput, TOutput>
    where TInput : IUseCaseInput
{
    Task<TOutput> HandleAsync(TInput input);
}
