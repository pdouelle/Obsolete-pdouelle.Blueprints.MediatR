using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Update
{
    public class UpdateCommandModel<TEntity, TUpdate> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }
        public TUpdate Request { get; set; }
    }
}