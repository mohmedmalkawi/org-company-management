using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

public static class UnitOfWorkDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        IApplicationDbContext context,
        ICommandHandler<TCommand, TResponse> innerHandler)
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            Result<TResponse> result = await innerHandler.Handle(command, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        IApplicationDbContext context,
        ICommandHandler<TCommand> innerHandler)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            Result result = await innerHandler.Handle(command, cancellationToken);

            await context.SaveChangesAsync(cancellationToken);

            return result;
        }
    }
}
