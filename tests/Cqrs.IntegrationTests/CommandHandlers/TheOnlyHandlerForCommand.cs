using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;

public class SingleHandlerForACommand : CommandHandlerBase<CommandWithSingleHandler>
{
    protected override Task<CommandResult> ExecuteCommandAsync(
        CommandWithSingleHandler command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandResult(CommandWithSingleHandler.StatusCodeSuccess, []));
    }
}