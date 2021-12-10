using Xunit;

namespace CodeReviewer.Tests
{
    public abstract class MainCodeReviewer
    {
        #region Pascal Code Reviewer

        [Fact]
        public virtual void PropertiesReview() => new PropertiesReviewer().PublicPropertiesOfClassesReview();

        [Fact]
        public virtual void TypePascalCodeReview() => new TypeReviewer().PublicTypesReview();

        [Fact]
        public virtual void MethodPascalCodeReview() => new MethodsReviewer().PublicMethodsOfClassesReview();
        #endregion

        #region Custom Code Reviewer

        [Fact]
        public virtual void SuffixCodeReview() => new CustomCodeReviewer().CheckCustomTypeSuffixNamingCodeReviewer();

        [Fact]
        public virtual void PrefixCodeReview() => new CustomCodeReviewer().CheckCustomTypePrefixNamingCodeReviewer();

        #endregion
    }
}
