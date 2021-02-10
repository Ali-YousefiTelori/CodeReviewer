using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// Camel codes of Method parameter check
    /// </summary>
    public class CamelCodeMethodParameterReviewer : CamelCodeBaseReviewer<ParameterInfo>
    {
        internal Type DeclaringType { get; set; }
        /// <summary>
        /// initialize
        /// </summary>
        public CamelCodeMethodParameterReviewer(Type declaringType)
        {
            ProviderName = "Method Parameter";
            DeclaringType = declaringType;
        }

        /// <summary>
        /// Review a parameter name of method
        /// </summary>
        /// <param name="parameter">parameter to check and review</param>
        /// <param name="builder">add errors to builder</param>
        public override bool Review(ParameterInfo parameter, StringBuilder builder)
        {
            return Review($"{DeclaringType.FullName} of parameter type of {parameter.ParameterType.FullName}", parameter.Name, builder);
        }
    }
}
