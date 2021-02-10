using CodeReviewer.Structures;
using System.Text;

namespace CodeReviewer.Reviewers.NamingConventions
{
    /// <summary>
    /// check the codes of Camel case
    /// </summary>
    /// <typeparam name="T">type of object to review</typeparam>
    public abstract class CamelCodeBaseReviewer<T> : IReviewer<T>
    {
        /// <summary>
        /// provider name is a name that help you undrestand what is checking as name
        /// </summary>
        protected string ProviderName { get; set; }

        /// <summary>
        /// review an object of camel cases
        /// </summary>
        /// <param name="reviewData"></param>
        /// <param name="builder"></param>
        public abstract bool Review(T reviewData, StringBuilder builder);

        /// <summary>
        /// review a name with a CamelCase
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="nameToReview">name to review</param>
        /// <param name="builder">save errors and messages in builder</param>
        /// <returns>when it has error will return true</returns>
        public bool Review(string pathName, string nameToReview, StringBuilder builder)
        {
            if (nameToReview == null || nameToReview.Length == 0)
            {
                builder.AppendLine($"You cannot review empty name of {pathName}");
                return true;
            }
            else if (char.IsUpper(nameToReview[0]))
            {
                builder.AppendLine($"Camel case of {ProviderName} of type {pathName} with name {nameToReview} is not a valid camel case naming conventions!");
                return true;
            }
            return false;
        }
    }
}
