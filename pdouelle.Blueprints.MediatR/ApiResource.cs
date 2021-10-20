using System;
using Ardalis.GuardClauses;
using pdouelle.Blueprints.MediatR.Attributes;

namespace pdouelle.Blueprints.MediatR
{
    public class ApiResource
    {
        public ApiResource()
        {
        }

        public ApiResource(Type entity, ApiResourceAttribute attribute)
        {
            Entity = entity;
            QueryList = attribute.QueryList;
            QuerySingle = attribute.QuerySingle;
            CustomQueryById = attribute.CustomQueryById;
            CustomCreate = attribute.CustomCreate;
            CustomUpdate = attribute.CustomUpdate;
            CustomDelete = attribute.CustomDelete;
        }

        public Type Entity { get; set; }
        public Type QueryList { get; set; }
        public Type QuerySingle { get; set; }
        
        public bool CustomQueryById { get; set; }
        public bool CustomCreate { get; set; }
        public bool CustomUpdate { get; set; }
        public bool CustomDelete { get; set; }
    }
}