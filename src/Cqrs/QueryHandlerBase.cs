using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MicroDotNet.Packages.Cqrs
{
    public abstract class QueryHandlerBase<TQuery, TResult> : IQueryHandler
        where TQuery : IQuery<TResult>
        where TResult : class
    {
        public async Task<object> FetchAsync(object query, CancellationToken cancellationToken)
        {
            if (query is null)
            {
                throw new ArgumentNullException(nameof(query));
            }
            
            if (!(query is TQuery cast))
            {
                var message = string.Format(
                    CultureInfo.InvariantCulture,
                    QueryHandlerBaseResources.InvalidQuery,
                    typeof(TQuery).FullName,
                    query.GetType().FullName);
                throw new ArgumentException(message, nameof(query));
            }
            
            return await this.FetchQueryAsync(cast, cancellationToken)
                .ConfigureAwait(false);
        }

        protected abstract Task<TResult> FetchQueryAsync(
            TQuery query,
            CancellationToken cancellationToken);
    }
}