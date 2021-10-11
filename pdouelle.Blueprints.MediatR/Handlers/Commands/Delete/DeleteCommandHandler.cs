using System.Threading;
using System.Threading.Tasks;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Delete;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Delete
{
    public class DeleteCommandHandler<TEntity, TDelete> : IRequestHandler<DeleteCommandModel<TEntity, TDelete>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public DeleteCommandHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(DeleteCommandModel<TEntity, TDelete> command, CancellationToken cancellationToken)
        {
            Repository.Delete(command.Entity);

            return command.Entity;
        }
    }
}