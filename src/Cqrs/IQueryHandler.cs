using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs
{
    public interface IQueryHandler
    {
        Task<object> FetchAsync(object query, CancellationToken cancellationToken);
    }
}