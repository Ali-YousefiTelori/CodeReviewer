using CodeReviewer.Engine;
using CodeReviewer.Samples;
using CodeReviewer.Tests;

namespace CodeReviewer.TestSamples
{
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            AssemblyManager.AddAssemblyToReview(typeof(PascalCaseSample));
        }
    }
}
