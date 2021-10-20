using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Queries.IdQuery;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.IdQuery
{
    public class IdQueryHandler<TEntity> : IRequestHandler<IdQueryModel<TEntity>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public IdQueryHandler(IRepository<TEntity> repository)
        {
            Guard.Against.Null(repository, nameof(repository));
            
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(IdQueryModel<TEntity> query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));
            
            TEntity entity = await Repository.GetByIdAsync(query.Id, cancellationToken);

            return entity;
        }
    }
}