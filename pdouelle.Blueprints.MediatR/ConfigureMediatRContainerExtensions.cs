using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Create;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Delete;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Patch;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Save;
using pdouelle.Blueprints.MediatR.Handlers.Commands.Update;
using pdouelle.Blueprints.MediatR.Handlers.Queries.IdQuery;
using pdouelle.Blueprints.MediatR.Handlers.Queries.ListQuery;
using pdouelle.GenericRepository;

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
                if (model.QueryById is not null)
                    yield return typeof(IdQueryHandler<,>).MakeGenericType(model.Entity, model.QueryById) as TypeInfo;
                
                if (model.QueryList is not null)
                    yield return typeof(ListQueryHandler<,>).MakeGenericType(model.Entity, model.QueryList) as TypeInfo;
                
                if (model.Create is not null)
                    yield return typeof(CreateCommandHandler<,>).MakeGenericType(model.Entity, model.Create) as TypeInfo;
                
                if (model.Update is not null)
                    yield return typeof(UpdateCommandHandler<,>).MakeGenericType(model.Entity, model.Update) as TypeInfo;
                
                if (model.Patch is not null)
                    yield return typeof(PatchCommandHandler<,>).MakeGenericType(model.Entity, model.Patch) as TypeInfo;
                
                if (model.Delete is not null)
                    yield return typeof(DeleteCommandHandler<,>).MakeGenericType(model.Entity, model.Delete) as TypeInfo;
                
                yield return typeof(SaveCommandHandler<>).MakeGenericType(model.Entity) as TypeInfo;
                
                foreach (Type dbContextType in dbContextTypes)
                {
                    yield return typeof(Repository<,>).MakeGenericType(model.Entity, dbContextType) as TypeInfo;
                }
            }
        }
    }
}