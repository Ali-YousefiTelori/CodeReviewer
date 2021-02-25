using System;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// check pascal case of types
    /// </summary>
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
        /// Review a type name
        /// </summary>
        /// <param name="type">type to check and review</param>
        /// <param name="builder">add errors to builder</param>
        public override bool Review(Type type, StringBuilder builder)
        {
            bool hasError = false;
            if (type.IsInterface)
            {
                var name = type.Name;
                if (name.Length <= 2)
                {
                    builder.AppendLine($"Interface length of name cannot be 2 or 1 of type {type.FullName}");
                    return true;
                }
                else if (!name.StartsWith("I"))
                {
                    builder.AppendLine($"You must add 'I' to start name of interface of {type.FullName}");
                    hasError = true;
                }

                name = name.Substring(1);
                if (Review($"{type.FullName}", name, builder))
                    hasError = true;
            }

            foreach (var name in type.FullName.Split('.'))
            {
                if (Review($"{type.FullName}", name, builder))
                    hasError = true;
            }

            return hasError;
        }
    }
}
