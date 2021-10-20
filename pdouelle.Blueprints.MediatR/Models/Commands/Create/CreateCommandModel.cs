using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Create
{
    public class CreateCommandModel<TEntity> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }
    }
}