using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        /// get list of assemblies
        /// </summary>
        /// <returns></returns>
        public static List<Assembly> GetAssemblies()
        {
            return Assemblies.ToList();
        } 
        
        /// <summary>
        /// get list of properties of assemblies types
        /// </summary>
        /// <returns>list of all types properties</returns>
        public static List<PropertyInfo> GetPublicProperties()
        {
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var assembly in GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    properties.AddRange(TypeManager.GetPublicProperties(type));
                }
            }
            return properties;
        }
    }
}
