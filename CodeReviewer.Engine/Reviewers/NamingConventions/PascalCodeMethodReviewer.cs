using CodeReviewer.Reviewers.NamingConventions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// Pascal codes of Methods check
    /// </summary>
    public class PascalCodeMethodReviewer : PascalCodeBaseReviewer<MethodInfo>
    {
        /// <summary>
        /// initialize
        /// </summary>
        public PascalCodeMethodReviewer()
        {
            ProviderName = "Method";
        }

        /// <summary>
        /// Review a method name
        /// </summary>
        /// <param name="method">method to check and review</param>
        /// <param name="builder">add errors to builder</param>
        public override bool Review(MethodInfo method, StringBuilder builder)
        {
            var hasError = Review($"{method.DeclaringType.FullName}", method.Name, builder);

            //review parameters of method
            foreach (var parameter in method.GetParameters())
            {
                if (new CamelCodeMethodParameterReviewer(method.DeclaringType).Review(parameter, builder))
                    hasError = true;
            }

            return hasError;
        }
    }
}