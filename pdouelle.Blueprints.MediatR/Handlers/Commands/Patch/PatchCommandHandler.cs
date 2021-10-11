using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Patch;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Patch
{
    public class PatchCommandHandler<TEntity, TPatch> : IRequestHandler<PatchCommandModel<TEntity, TPatch>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;
        private readonly IMapper _mapper;

        public PatchCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TEntity> Handle(PatchCommandModel<TEntity, TPatch> command, CancellationToken cancellationToken)
        {
            _mapper.Map(command.Request, command.Entity);

            Repository.Edit(command.Entity);

            return command.Entity;
        }
    }
}