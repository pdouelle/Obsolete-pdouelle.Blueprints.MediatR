using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Delete;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Delete
{
    public class DeleteCommandHandler<TEntity> : IRequestHandler<DeleteCommandModel<TEntity>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public DeleteCommandHandler(IRepository<TEntity> repository)
        {
            Guard.Against.Null(repository, nameof(repository));
            
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(DeleteCommandModel<TEntity> command, CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(command));
            Guard.Against.Null(command.Entity, nameof(command.Entity));
            
            Repository.Remove(command.Entity);

            return command.Entity;
        }
    }
}