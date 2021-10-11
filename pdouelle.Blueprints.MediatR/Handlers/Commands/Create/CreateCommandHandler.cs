using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Create;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Create
{
    public class CreateCommandHandler<TEntity, TCreate> : IRequestHandler<CreateCommandModel<TEntity, TCreate>, TEntity>
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;
        private readonly IMapper _mapper;

        public CreateCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TEntity> Handle(CreateCommandModel<TEntity, TCreate> command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<TEntity>(command.Request);

            Repository.Create(entity);

            return entity;
        }
    }
}