using System;
using MediatR;

namespace pdouelle.Blueprints.MediatR.Models.Queries.IdQuery
{
    public class IdQueryModel<TEntity> : IRequest<TEntity>
    {
        public Guid Id { get; set; }

        public IdQueryModel()
        {
            
        }

        public IdQueryModel(Guid id)
        {
            Id = id;
        }
    }
}