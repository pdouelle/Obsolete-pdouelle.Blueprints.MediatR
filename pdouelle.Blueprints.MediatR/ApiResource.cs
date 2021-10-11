using System;
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
            QueryById = attribute.QueryById;
            Create = attribute.Create;
            Update = attribute.Update;
            Patch = attribute.Patch;
            Delete = attribute.Delete;
        }

        public Type Entity { get; set; }
        public Type QueryList { get; set; }
        public Type QueryById { get; set; }
        public Type Create { get; set; }
        public Type Update { get; set; }
        public Type Patch { get; set; }
        public Type Delete { get; set; }
    }
}