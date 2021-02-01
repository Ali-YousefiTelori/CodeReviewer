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
        public override bool Review(Type type, StringBuilder builder)
        {
            foreach (var name in type.FullName.Split('.'))
            {
                //When has error break and don't duplication
                if (Review($"{type.FullName}", name, builder))
                    return true;
            }
            return false;
        }
    }
}
