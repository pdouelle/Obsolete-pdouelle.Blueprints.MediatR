using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pdouelle.Blueprints.MediatR.Models.Queries.SingleQuery;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;
using pdouelle.LinqExtensions;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.SingleQuery
{
    public class SingleQueryHandler<TEntity, TQuerySingle> : IRequestHandler<SingleQueryModel<TEntity,TQuerySingle>, TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public SingleQueryHandler(IRepository<TEntity> repository)
        {
            Guard.Against.Null(repository, nameof(repository));
            
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(SingleQueryModel<TEntity,TQuerySingle> query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(query.Request, nameof(query.Request));
            
            IQueryable<TEntity> queryable = Repository.GetAll();

            queryable = queryable.FilterByModel(query.Request);

            TEntity entity = await queryable.SingleOrDefaultAsync(cancellationToken);

            return entity;
        }
    }
}