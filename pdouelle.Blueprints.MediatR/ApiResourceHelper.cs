using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using pdouelle.Blueprints.MediatR.Attributes;

namespace pdouelle.Blueprints.MediatR
{
    public static class ApiResourceHelper
    {
        public static Assembly[] Assemblies { get; set; }

        public static IEnumerable<ApiResource> GetModel()
        {
            foreach (Assembly assembly in Assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    Attribute attribute = type.GetCustomAttributes(typeof(ApiResourceAttribute)).SingleOrDefault();

                    if (attribute is ApiResourceAttribute apiResourceAttribute)
                    {
                        var apiResourceType = new ApiResource(type, apiResourceAttribute);
                        
                        yield return apiResourceType;
                    }
                }
            }
        }
    }
}