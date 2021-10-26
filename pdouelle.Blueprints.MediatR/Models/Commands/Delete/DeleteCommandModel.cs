using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Delete
{
    public class DeleteCommandModel<TEntity> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }

        public DeleteCommandModel()
        {
            
        }

        public DeleteCommandModel(TEntity entity)
        {
            Entity = entity;
        }
    }
}