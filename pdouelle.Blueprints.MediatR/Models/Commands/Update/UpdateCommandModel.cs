using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Commands.Update
{
    public class UpdateCommandModel<TEntity> : IRequest<TEntity>
    {
        public TEntity Entity { get; set; }

        public UpdateCommandModel()
        {
            
        }

        public UpdateCommandModel(TEntity entity)
        {
            Entity = entity;
        }
    }
}