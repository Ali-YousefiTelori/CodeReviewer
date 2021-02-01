using CodeReview.Engine;
using CodeReview.Samples;
using CodeReview.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodeReview.TestSamples
{
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            AssemblyManager.AddAssemblyToReview(typeof(PascalCaseSample));
        }
    }
}
