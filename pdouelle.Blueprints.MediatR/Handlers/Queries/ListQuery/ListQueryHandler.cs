using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.ListQuery
{
    public class ListQueryHandler<TEntity, TQueryList> : IRequestHandler<ListQueryModel<TEntity, TQueryList>, IEnumerable<TEntity>> 
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public ListQueryHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> Handle(ListQueryModel<TEntity, TQueryList> listQuery, CancellationToken cancellationToken)
        {
            return await Repository.GetAllAsync(cancellationToken);
        }
    }
}