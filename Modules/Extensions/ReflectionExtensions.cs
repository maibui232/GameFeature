namespace Modules.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class ReflectionExtensions
    {
        public static IEnumerable<Type> GetAllDerivedTypes(this AppDomain aAppDomain, Type aType)
        {
            var assemblies = aAppDomain.GetAssemblies();

            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type.IsSubclassOf(aType) && !type.IsAbstract
                   select type;
        }

        public static IEnumerable<Type> GetAllDerivedTypes<T>(this AppDomain aAppDomain)
        {
            return GetAllDerivedTypes(aAppDomain, typeof(T));
        }

        public static IEnumerable<Type> GetTypesWithInterface(this AppDomain aAppDomain, Type aInterfaceType)
        {
            var result     = new List<Type>();
            var assemblies = aAppDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var types = assembly.GetTypes();
                result.AddRange(types.Where(type => aInterfaceType.IsAssignableFrom(type) && !type.IsAbstract));
            }

            return result;
        }

        public static IEnumerable<Type> GetTypesWithInterface<T>(this AppDomain aAppDomain)
        {
            return GetTypesWithInterface(aAppDomain, typeof(T));
        }
    }
}