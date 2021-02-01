using System;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    public class PascalCodeTypeReviewer : PascalCodeBaseReviewer<Type>
    {
        /// <summary>
        /// initialize
        /// </summary>
        public PascalCodeTypeReviewer()
        {
            ProviderName = "Type";
        }

        /// <summary>
        /// Review a property name
        /// </summary>
        /// <param name="property">property to check and review</param>
        /// <param name="builder">add errors to builder</param>
        public override void Review(Type type, StringBuilder builder)
        {
            Review($"{type.FullName}", type.Name, builder);
        }
    }
}
