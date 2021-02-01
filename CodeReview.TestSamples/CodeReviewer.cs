using CodeReview.Engine;
using CodeReview.Samples;
using CodeReview.Tests;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CodeReview.TestSamples
{
    public class CodeReviewer : MainCodeReviewer
    {
        static CodeReviewer()
        {
            AssemblyManager.AddAssemblyToReview(typeof(PascalCaseSample));
        }
    }
}
