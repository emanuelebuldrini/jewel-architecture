
using JewelArchitecture.Examples.SmartCharging.Application.UseCases.Input;

namespace JewelArchitecture.Examples.SmartCharging.Application.UseCases;

public interface IUseCase<TInput,TOutput> 
    where TInput : IUseCaseInput
{
    Task<TOutput> HandleAsync(TInput input);
}
