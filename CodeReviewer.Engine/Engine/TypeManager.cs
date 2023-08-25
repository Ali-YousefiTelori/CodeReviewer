using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CodeReviewer.Engine
{
    /// <summary>
    /// manage types at runtime
    /// </summary>
    public static class TypeManager
    {
        /// <summary>
        /// if type are in assemblt manager
        /// </summary>
        /// <returns></returns>
        public static bool CanReview(Type type)
        {
            return AssemblyManager.GetCachedPublicTypes().Any(x => x == type);
        }

        /// <summary>
        /// get list of methods of type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<MethodInfo> GetPublicMethods(this Type type)
        {
            List<MethodInfo> objectMethods = typeof(object).GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static).ToList();
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)
                .Where(x => !x.IsSpecialName)
                .Where(x => !objectMethods.Any(om => om.Name == x.Name))
                .Where(x => CanReview(x.DeclaringType))
                .ToList();
        }

        /// <summary>
        /// get list of public properties
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static List<PropertyInfo> GetPublicProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(x => CanReview(x.DeclaringType))
                .ToList();
        }
    }
}
