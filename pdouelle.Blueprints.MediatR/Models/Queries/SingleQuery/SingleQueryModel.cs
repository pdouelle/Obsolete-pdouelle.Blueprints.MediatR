using MediatR;
using pdouelle.LinqExtensions.Interfaces;

namespace pdouelle.Blueprints.MediatR.Models.Queries.SingleQuery
{
    public class SingleQueryModel<TEntity, TQuerySingle> : IRequest<TEntity>
        where TQuerySingle : IInclude
    {
        public TQuerySingle Request { get; set; }

        public SingleQueryModel()
        {
            
        }

        public SingleQueryModel(TQuerySingle request)
        {
            Request = request;
        }
    }
}