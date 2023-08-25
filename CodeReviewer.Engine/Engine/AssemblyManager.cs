using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace CodeReviewer.Engine
{
    /// <summary>
    /// Manage your assemblies to review your codes
    /// </summary>
    public static class AssemblyManager
    {
        /// <summary>
        /// assemblies added for review
        /// </summary>
        static List<Assembly> Assemblies { get; set; } = new List<Assembly>();
        static List<Stream> Streams { get; set; } = new List<Stream>();

        /// <summary>
        /// add list of assemblies to review
        /// </summary>
        /// <param name="assemblies">assemblies to review</param>
        public static void AddAssemblyToReview(params Assembly[] assemblies)
        {
            Assemblies.AddRange(assemblies.Distinct().Where(x => !Assemblies.Contains(x)));
        }

        /// <summary>
        /// add list of types as assemblies to review
        /// this method will get assemblies of types and add assemblies to review
        /// </summary>
        /// <param name="types">types that you want to add assemblies of types</param>
        public static void AddAssemblyToReview(params Type[] types)
        {
            AddAssemblyToReview(types.Select(x => x.Assembly).ToArray());
        }     

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streams"></param>
        public static void AddStreamsToReview(params Stream[] streams)
        {
            Streams.AddRange(streams);
        }

        /// <summary>
        /// 
        /// </summary>
        public static List<Stream> GetStreams()
        {
            return Streams;
        }

        /// <summary>
        /// get list of assemblies
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies()
        {
            return Assemblies.ToList();
        }

        static List<Type> CachedTypes { get; set; }
        /// <summary>
        /// get list of types of assemblies
        /// </summary>
        /// <returns>list of all types</returns>
        public static List<Type> GetPublicTypes()
        {
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (CustomCodeReviewerManager.SkippedTypesCodeReviewers.Contains(type))
                        continue;
                    types.Add(type);
                }
            }
            return types;
        }

        /// <summary>
        /// get list of cached types of assemblies
        /// </summary>
        /// <returns>list of all types</returns>
        public static List<Type> GetCachedPublicTypes()
        {
            if (CachedTypes == null)
                CachedTypes = GetPublicTypes();
            return CachedTypes;
        }

        /// <summary>
        /// get list of properties of assemblies types
        /// </summary>
        /// <returns>list of all types properties</returns>
        public static List<PropertyInfo> GetPublicProperties()
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (Assembly assembly in GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    properties.AddRange(TypeManager.GetPublicProperties(type));
                }
            }
            return properties;
        }

        /// <summary>
        /// get list of methods of assemblies types
        /// </summary>
        /// <returns>list of all types properties</returns>
        public static List<MethodInfo> GetMethodsOfProperties()
        {
            List<MethodInfo> properties = new List<MethodInfo>();
            foreach (Assembly assembly in GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    properties.AddRange(TypeManager.GetPublicMethods(type));
                }
            }
            return properties;
        }
    }
}
