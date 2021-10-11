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
            ApiResourceType[] apiResources = ApiResources.GetApiResources().ToArray();

            IEnumerable<TypeInfo> handlers = GetHandlersTypes(apiResources, dbContexts);
            
            foreach (TypeInfo handler in handlers)
            {
                foreach (Type apiResource in handler.ImplementedInterfaces)
                {
                    builder.RegisterType(handler).As(apiResource);
                }
            }
        }

        private static IEnumerable<TypeInfo> GetHandlersTypes(IEnumerable<ApiResourceType> apiResourceTypes, Type[] dbContextTypes)
        {
            foreach (ApiResourceType apiResourceType in apiResourceTypes)
            {
                if (apiResourceType.QueryById is not null)
                    yield return typeof(IdQueryHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.QueryById) as TypeInfo;
                
                if (apiResourceType.QueryList is not null)
                    yield return typeof(ListQueryHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.QueryList) as TypeInfo;
                
                if (apiResourceType.Create is not null)
                    yield return typeof(CreateCommandHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.Create) as TypeInfo;
                
                if (apiResourceType.Update is not null)
                    yield return typeof(UpdateCommandHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.Update) as TypeInfo;
                
                if (apiResourceType.Patch is not null)
                    yield return typeof(PatchCommandHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.Patch) as TypeInfo;
                
                if (apiResourceType.Delete is not null)
                    yield return typeof(DeleteCommandHandler<,>).MakeGenericType(apiResourceType.Entity, apiResourceType.Delete) as TypeInfo;
                
                yield return typeof(SaveCommandHandler<>).MakeGenericType(apiResourceType.Entity) as TypeInfo;
                
                foreach (Type dbContextType in dbContextTypes)
                {
                    yield return typeof(Repository<,>).MakeGenericType(apiResourceType.Entity, dbContextType) as TypeInfo;
                }
            }
        }
    }
}