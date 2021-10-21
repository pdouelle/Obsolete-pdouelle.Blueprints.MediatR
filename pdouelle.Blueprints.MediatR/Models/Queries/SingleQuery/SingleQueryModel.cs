using MediatR;
using pdouelle.Sort;

namespace pdouelle.Blueprints.MediatR.Models.Queries.SingleQuery
{
    public class SingleQueryModel<TEntity, TQuerySingle> : IRequest<TEntity>
    {
        public TQuerySingle Request { get; set; }
    }
}