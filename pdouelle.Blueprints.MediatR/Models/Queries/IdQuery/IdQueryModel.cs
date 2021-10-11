using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Queries.IdQuery
{
    public class IdQueryModel<TEntity, TQueryById> : IRequest<TEntity>
    {
        public TQueryById Request { get; set; }
    }
}