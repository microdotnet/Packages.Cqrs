using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;

public class SecondConflictingCommandHandler : CommandHandlerBase<CommandWithConflictingHandlers>
{
    protected override Task<CommandResult> ExecuteCommandAsync(CommandWithConflictingHandlers command, CancellationToken cancellationToken)
    {
        return Task.FromResult<CommandResult>(new(2, []));
    }
}