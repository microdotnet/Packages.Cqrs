using MicroDotNet.Packages.Cqrs.IntegrationTests.Commands;

namespace MicroDotNet.Packages.Cqrs.IntegrationTests.CommandHandlers;

public class Command1Handler : CommandHandlerBase<Command1>
{
    protected override Task<CommandResult> ExecuteCommandAsync(
        Command1 command,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new CommandResult(Command1.StatusCodeSuccess, []));
    }
}