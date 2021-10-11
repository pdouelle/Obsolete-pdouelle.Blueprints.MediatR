using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Create
{
    public class CreateCommandModel<TEntity, TCreate> : IRequest<TEntity>
    {
        public TCreate Request { get; set; }
    }
}