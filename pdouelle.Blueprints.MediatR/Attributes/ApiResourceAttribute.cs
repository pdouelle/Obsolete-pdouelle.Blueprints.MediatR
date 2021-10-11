using System;

namespace pdouelle.Blueprints.MediatR.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiResourceAttribute : Attribute
    {
        public Type QueryList { get; set; }
        public Type QueryById { get; set; }
        public Type Create { get; set; }
        public Type Update { get; set; }
        public Type Patch { get; set; }
        public Type Delete { get; set; }
    }
}