namespace JewelArchitecture.Core.Application.UseCases;

public interface IUseCase<TInput, TOutput>
    where TInput : IUseCaseInput
{
    Task<TOutput> HandleAsync(TInput input);
}
