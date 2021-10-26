using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Queries.SingleQuery
{
    public class SingleQueryModel<TEntity, TQuerySingle> : IRequest<TEntity>
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