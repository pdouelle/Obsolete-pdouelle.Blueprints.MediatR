using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Create;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Create
{
    public class CreateCommandHandler<TEntity> : IRequestHandler<CreateCommandModel<TEntity>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public CreateCommandHandler(IRepository<TEntity> repository)
        {
            Guard.Against.Null(repository, nameof(repository));
            
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(CreateCommandModel<TEntity> command, CancellationToken cancellationToken)
        {
            Guard.Against.Null(command, nameof(command));
            Guard.Against.Null(command.Entity, nameof(command.Entity));
            
            await Repository.AddAsync(command.Entity, cancellationToken);

            return command.Entity;
        }
    }
}