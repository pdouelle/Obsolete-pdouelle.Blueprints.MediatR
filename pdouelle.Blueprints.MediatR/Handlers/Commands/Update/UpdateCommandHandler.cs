using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using pdouelle.Blueprints.MediatR.Models.Commands.Update;
using pdouelle.Entity;
using pdouelle.GenericRepository;

namespace pdouelle.Blueprints.MediatR.Handlers.Commands.Update
{
    public class UpdateCommandHandler<TEntity, TUpdate> : IRequestHandler<UpdateCommandModel<TEntity, TUpdate>, TEntity> 
        where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> Repository;
        private readonly IMapper _mapper;

        public UpdateCommandHandler(IRepository<TEntity> repository, IMapper mapper)
        {
            Repository = repository;
            _mapper = mapper;
        }

        public virtual async Task<TEntity> Handle(UpdateCommandModel<TEntity, TUpdate> command, CancellationToken cancellationToken)
        {
            _mapper.Map(command.Request, command.Entity);

            Repository.Edit(command.Entity);

            return command.Entity;
        }
    }
}