namespace JewelArchitecture.Core.Application.UseCases;

public abstract class NoOutputUseCase<TInput> : IUseCase<TInput, EmptyOutput>
    where TInput : IUseCaseInput
{
    public async Task<EmptyOutput> HandleAsync(TInput input)
    {
        await HandleNoOutputAsync(input);

        return new EmptyOutput();
    }

    protected abstract Task HandleNoOutputAsync(TInput input);
}