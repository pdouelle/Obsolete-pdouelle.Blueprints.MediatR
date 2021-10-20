using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Update;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Update
{
    public class UpdateCommandHandler<TEntity> : IRequestHandler<UpdateCommandModel<TEntity>, TEntity> 
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public UpdateCommandHandler(IRepository<TEntity> repository)
        {
            Guard.Against.Null(repository, nameof(repository));
            
            Repository = repository;
        }

        public virtual async Task<TEntity> Handle(UpdateCommandModel<TEntity> command, CancellationToken cancellationToken)
        {
            Repository.Update(command.Entity);

            return command.Entity;
        }
    }
}