using CodeReviewer.Engine;
using CodeReviewer.Samples;
using CodeReviewer.Tests;

namespace CodeReviewer.TestSamples
{
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            //types to check (this will check all of types in assembly so no need to add all of types of assembly)
            AssemblyManager.AddAssemblyToReview(typeof(pascalCaseSample));
        }
    }
}
