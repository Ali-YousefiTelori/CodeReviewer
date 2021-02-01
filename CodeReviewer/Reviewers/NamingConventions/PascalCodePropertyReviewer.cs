using System.Reflection;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// Pascal codes of property checks
    /// </summary>
    public class PascalCodePropertyReviewer : PascalCodeBaseReviewer<PropertyInfo>
    {
        /// <summary>
        /// initialize
        /// </summary>
        public PascalCodePropertyReviewer()
        {
            ProviderName = "Property";
        }

        /// <summary>
        /// Review a property name
        /// </summary>
        /// <param name="property">property to check and review</param>
        /// <param name="builder">add errors to builder</param>
        public override void Review(PropertyInfo property, StringBuilder builder)
        {
            Review($"{property.DeclaringType.FullName}", property.Name, builder);
        }
    }
}
