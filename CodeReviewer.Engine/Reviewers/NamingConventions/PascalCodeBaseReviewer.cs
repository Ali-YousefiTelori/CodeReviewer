using CodeReviewer.Structures;
using System;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// check the codes of Pascal
    /// </summary>
    /// <typeparam name="T">type of object to review</typeparam>
    public abstract class PascalCodeBaseReviewer<T> : IReviewer<T>
    {
        /// <summary>
        /// provider name is a name that help you undrestand what is checking as name
        /// </summary>
        protected string ProviderName { get; set; }
        /// <summary>
        /// review an object of pascal cases
        /// </summary>
        /// <param name="reviewData"></param>
        /// <param name="builder"></param>
        public abstract bool Review(T reviewData, StringBuilder builder);

        /// <summary>
        /// review a name with a PascalCase
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="nameToReview">name to review</param>
        /// <param name="builder">save errors and messages in builder</param>
        /// <returns>when it has error will return true</returns>
        public virtual bool Review(Type declaringType, string pathName, string nameToReview, StringBuilder builder)
        {
            if (declaringType == null)
                return false;
            if (nameToReview == null || nameToReview.Length == 0)
            {
                builder.AppendLine($"You cannot review empty name of {GetPathNameDetails(declaringType, pathName)}");
                return true;
            }
            else if (!char.IsUpper(nameToReview[0]))
            {
                builder.AppendLine($"Pascal case of {ProviderName} of type {GetPathNameDetails(declaringType, pathName)} with name {nameToReview} is not a valid pascal case naming conventions!");
                return true;
            }
            return false;
        }

        string GetPathNameDetails(Type declaringType, string pathName)
        {
            if (pathName.StartsWith("<>f__AnonymousType"))
            {
                return $"AnonymousType generated in your codes with name of {pathName} in assembly {declaringType?.Assembly?.FullName} (That you can find and fix it)";
            }
            else
                return pathName;
        }
    }
}
