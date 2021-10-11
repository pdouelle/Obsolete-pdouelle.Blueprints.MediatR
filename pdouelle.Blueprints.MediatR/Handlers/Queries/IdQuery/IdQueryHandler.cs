using System.Threading;
using System.Threading.Tasks;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Queries.IdQuery;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.IdQuery
{
    public class IdQueryHandler<TEntity, TQueryById> : IRequestHandler<IdQueryModel<TEntity, TQueryById>, TEntity>
        where TEntity : IEntity
        where TQueryById : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public IdQueryHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(IdQueryModel<TEntity, TQueryById> query, CancellationToken cancellationToken)
        {
            return await Repository.GetByIdAsync(query.Request.Id);
        }
    }
}