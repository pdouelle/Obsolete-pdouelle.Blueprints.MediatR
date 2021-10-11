using System.Threading;
using System.Threading.Tasks;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Save;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Save
{
    public class SaveCommandHandler<TEntity> : IRequestHandler<SaveCommandModel<TEntity>, bool> 
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;

        public SaveCommandHandler(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual async Task<bool> Handle(SaveCommandModel<TEntity> command, CancellationToken cancellationToken)
        {
            return await Repository.SaveAsync(cancellationToken);
        }
    }
}