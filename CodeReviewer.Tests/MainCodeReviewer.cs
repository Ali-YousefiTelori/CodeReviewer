using Xunit;

namespace CodeReviewer.Tests
{
    public abstract class MainCodeReviewer
    {
        #region Properties Reviewer

        [Fact]
        public void PropertiesReview() => new PropertiesReviewer().PublicPropertiesOfClasses();

        #endregion
    }
}
