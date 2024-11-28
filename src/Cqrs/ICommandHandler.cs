using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs
{
    public interface ICommandHandler
    {
        Task<CommandResult> ExecuteAsync(object command, CancellationToken cancellationToken);
    }
}