using MediatR;
using pdouelle.Entity;
using pdouelle.LinqExtensions.Interfaces;
using pdouelle.Pagination;
using pdouelle.Sort;

namespace pdouelle.Blueprints.MediatR.Models.Queries.ListQuery
{
    public class ListQueryModel<TEntity, TQueryList> : IRequest<PagedList<TEntity>> 
        where TEntity : IEntity
        where TQueryList : IPagination, ISort, IInclude
    {
        public TQueryList Request { get; set; }

        public ListQueryModel()
        {
            
        }

        public ListQueryModel(TQueryList request)
        {
            Request = request;
        }
    }
}