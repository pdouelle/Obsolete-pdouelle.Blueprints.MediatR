using System.Collections.Generic;
using MediatR;
using pdouelle.Entity;

namespace pdouelle.Blueprints.MediatR.Models.Queries.ListQuery
{
    public class ListQueryModel<TEntity, TQuery> : IRequest<IEnumerable<TEntity>> where TEntity : IEntity
    {
        public TQuery Request { get; set; }
    }
}