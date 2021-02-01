using CodeReviewer.Structures;
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
        public abstract void Review(T reviewData, StringBuilder builder);

        /// <summary>
        /// review a name with a PascalCase
        /// </summary>
        /// <param name="pathName"></param>
        /// <param name="name">name to review</param>
        /// <param name="builder">save errors and messages in builder</param>
        public void Review(string pathName, string name, StringBuilder builder)
        {
            if (name == null || name.Length == 0)
                builder.AppendLine($"You cannot review empty name of {pathName}");
            else if (!char.IsUpper(name[0]))
                builder.AppendLine($"Pascal case of {ProviderName} of type {pathName} with name {name} is not a valid pascal case naming conventions!");
        }
    }
}
