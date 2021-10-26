using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Create;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Delete;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Save;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Update;
using pdouelle.Blueprints.MediatR.Handlers.Queries.ExistsQuery;
using pdouelle.Blueprints.MediatR.Handlers.Queries.IdQuery;
using pdouelle.Blueprints.MediatR.Handlers.Queries.ListQuery;
using pdouelle.Blueprints.MediatR.Handlers.Queries.SingleQuery;
using pdouelle.Blueprints.Repositories;

namespace pdouelle.Blueprints.MediatR
{
    public static class ConfigureMediatRContainerExtensions
    {
        public static void ConfigureContainer(this ContainerBuilder builder, params Type[] dbContexts)
        {
            ApiResource[] models = ApiResourceHelper.GetModel().ToArray();

            IEnumerable<TypeInfo> genericHandlers = GetGenericHandler(models, dbContexts);
            
            foreach (TypeInfo genericHandler in genericHandlers)
            {
                foreach (Type mediatRRequestHandler in genericHandler.ImplementedInterfaces)
                {
                    builder.RegisterType(genericHandler).As(mediatRRequestHandler);
                }
            }
        }

        private static IEnumerable<TypeInfo> GetGenericHandler(IEnumerable<ApiResource> models, Type[] dbContextTypes)
        {
            foreach (ApiResource model in models)
            {
                if (model.CustomQueryById is false)
                    yield return typeof(IdQueryHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                if (model.QueryList is not null)
                    yield return typeof(ListQueryHandler<,>).MakeGenericType(model.Entity, model.QueryList) as TypeInfo;
                
                if (model.QuerySingle is not null)
                    yield return typeof(SingleQueryHandler<,>).MakeGenericType(model.Entity, model.QuerySingle) as TypeInfo;
                
                if (model.CustomCreate is false)
                    yield return typeof(CreateCommandHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                if (model.CustomUpdate is false)
                    yield return typeof(UpdateCommandHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                if (model.CustomDelete is false)
                    yield return typeof(DeleteCommandHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                yield return typeof(ExistsQueryHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                yield return typeof(SaveCommandHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                foreach (Type dbContextType in dbContextTypes)
                {
                    yield return typeof(Repository<,>).MakeGenericType(model.Entity, dbContextType) as TypeInfo;
                }
            }
        }
    }
}