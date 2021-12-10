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
        /// <returns>When it has error return true</returns>
        bool Review(T reviewData, StringBuilder builder);
    }

    /// <summary>
    /// Build a Viewer for a work
    /// </summary>
    public interface IReviewer
    {
        /// <summary>
        /// do the review of code
        /// </summary>
        /// <param name="builder">add exceptions and reviews of codes on this builder</param>
        /// <param name="reviewData">data of object to review</param>
        /// <returns>When it has error return true</returns>
        bool Review(object reviewData, StringBuilder builder);
    }
}
