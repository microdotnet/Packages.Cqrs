using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;

public class SecondMultitenantCommandHandler : CommandHandlerBase<MultitenantCommand>
{
    protected override Task<CommandResult> ExecuteCommandAsync(MultitenantCommand command, CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandResult(2, []));
    }
}