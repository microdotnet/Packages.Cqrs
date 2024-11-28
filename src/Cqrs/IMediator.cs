using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs
{
    public interface IMediator
    {
        Task<CommandResult> ExecuteAsync<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : ICommand;

        Task<TResult> FetchAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken)
            where TResult : class;
    }
}