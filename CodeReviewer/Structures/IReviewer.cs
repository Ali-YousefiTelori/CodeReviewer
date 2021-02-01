using System.Text;

namespace CodeReviewer.Structures
{
    /// <summary>
    /// Build a Viewer for a work
    /// </summary>
    /// <typeparam name="T">type of object to review</typeparam>
    public interface IReviewer<T>
    {
        /// <summary>
        /// do the review of code
        /// </summary>
        /// <param name="builder">add exceptions and reviews of codes on this builder</param>
        /// <param name="reviewData">data of object to review</param>
        void Review(T reviewData, StringBuilder builder);
    }
}
