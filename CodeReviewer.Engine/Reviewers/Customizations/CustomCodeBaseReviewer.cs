using CodeReviewer.Structures;
using System.Text;

namespace CodeReviewer.Reviewers.Customizations
{
    public abstract class CustomCodeBaseReviewer<T> : IReviewer<T>, IReviewer
    {
        public abstract bool Review(T reviewData, StringBuilder builder);

        public bool Review(object reviewData, StringBuilder builder)
        {
            return Review((T)reviewData, builder);
        }
    }
}
