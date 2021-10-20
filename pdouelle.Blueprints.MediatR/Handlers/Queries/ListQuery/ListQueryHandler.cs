using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Queries.ListQuery;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;
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
            Guard.Against.Null(repository, nameof(repository));

            Repository = repository;
        }

        public virtual async Task<PagedList<TEntity>> Handle(ListQueryModel<TEntity, TQueryList> query, CancellationToken cancellationToken)
        {
            Guard.Against.Null(query, nameof(query));
            Guard.Against.Null(query.Request, nameof(query.Request));
            
            IQueryable<TEntity> queryable = Repository.GetAll();

            queryable = queryable.FilterByModel(query.Request);
            
            return await PagedList<TEntity>.ToPagedListAsync(queryable, query.Request.PageNumber, query.Request.PageSize, cancellationToken);
        }
    }
}