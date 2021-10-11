using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Delete
{
    public class DeleteCommandModel<TEntity, TDelete> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }
        public TDelete Request { get; set; }
    }
}