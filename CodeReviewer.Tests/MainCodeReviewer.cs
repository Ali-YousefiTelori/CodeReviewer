using Xunit;

namespace CodeReviewer.Tests
{
    public abstract class MainCodeReviewer
    {
        #region Pascal Code Reviewer

        [Fact]
        public void PropertiesReview() => new PropertiesReviewer().PublicPropertiesOfClassesReview();

        [Fact]
        public void TypePascalCodeReview() => new TypeReviewer().PublicTypesReview();

        [Fact]
        public void MethodPascalCodeReview() => new MethodsReviewer().PublicMethodsOfClassesReview();
        #endregion

    }
}
