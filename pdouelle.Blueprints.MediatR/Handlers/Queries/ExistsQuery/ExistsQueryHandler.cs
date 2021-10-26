using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using pdouelle.Blueprints.MediatR.Models.Queries.ExistsQuery;
using pdouelle.Blueprints.Repositories;
using pdouelle.Entity;
using pdouelle.LinqExtensions.Helpers;

namespace pdouelle.Blueprints.MediatR.Handlers.Queries.ExistsQuery
{
    public class ExistsQueryHandler<TResource> : IRequestHandler<ExistsQueryModel<TResource>, bool>
        where TResource : IEntity
    {
        private readonly IRepository<TResource> _repository;

        public ExistsQueryHandler(IRepository<TResource> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(ExistsQueryModel<TResource> request, CancellationToken cancellationToken)
        {
            Guard.Against.Null(request, nameof(request));
            Guard.Against.Null(request.KeyValues, nameof(request.KeyValues));

            IQueryable<TResource> queryable = _repository.GetAll();

            foreach (var (propertyName, propertyValue) in request.KeyValues)
            {
                Expression<Func<TResource, bool>> predicate = WhereHelper.CreateEqualWherePredicate<TResource>(propertyName, propertyValue);
                
                queryable = queryable.Where(predicate);
            }

            var exist = await queryable.AnyAsync(cancellationToken);

            return exist;
        }
    }
}