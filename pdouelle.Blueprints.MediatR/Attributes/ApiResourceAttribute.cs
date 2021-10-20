using System;

namespace pdouelle.Blueprints.MediatR.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiResourceAttribute : Attribute
    {
        public Type QueryList { get; set; }
        public Type QuerySingle { get; set; }
        public bool CustomQueryById { get; set; }
        public bool CustomCreate { get; set; }
        public bool CustomUpdate { get; set; }
        public bool CustomDelete { get; set; }
    }
}