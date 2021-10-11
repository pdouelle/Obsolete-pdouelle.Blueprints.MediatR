using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
using pdouelle.Entity;
using pdouelle.GenericRepository;
using pdouelle.LinqExtensions;
using pdouelle.Pagination;
using pdouelle.Sort;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.ListQuery
{
    public class ListQueryHandler<TEntity, TQueryList> : IRequestHandler<ListQueryModel<TEntity, TQueryList>, PagedList<TEntity>> 
        where TEntity : class, IEntity 
        where TQueryList : IPagination, ISort
    {
        protected readonly IRepository<TEntity> Repository;

        public ListQueryHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<PagedList<TEntity>> Handle(ListQueryModel<TEntity, TQueryList> query, CancellationToken cancellationToken)
        {
            TQueryList model = query.Request;
            
            IQueryable<TEntity> queryable = Repository.Filter();

            queryable = queryable.FilterByModel(model);
            
            return await PagedList<TEntity>.ToPagedListAsync(queryable, model.PageNumber, model.PageSize);
        }
    }
}