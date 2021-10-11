using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace pdouelle.Blueprints.MediatR
{
    public static class ApiResources
    {
        public static Assembly[] Assemblies { get; set; }

        public static IEnumerable<ApiResourceType> GetApiResources()
        {
            foreach (Assembly assembly in Assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    Attribute attribute = type.GetCustomAttributes(typeof(ApiResourceAttribute)).SingleOrDefault();

                    if (attribute is ApiResourceAttribute apiResourceAttribute)
                    {
                        var apiResourceType = new ApiResourceType(type, apiResourceAttribute);
                        
                        yield return apiResourceType;
                    }
                }
            }
        }
    }
}