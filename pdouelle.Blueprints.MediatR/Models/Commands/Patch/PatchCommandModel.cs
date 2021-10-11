using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Patch
{
    public class PatchCommandModel<TEntity, TPatch> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }
        public TPatch Request { get; set; }
    }
}