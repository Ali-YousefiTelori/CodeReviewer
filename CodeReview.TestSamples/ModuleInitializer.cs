using CodeReview.Engine;
using CodeReview.Samples;
using CodeReview.Tests;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReview.TestSamples
{
    /// <summary>
    /// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
    /// </summary>
    public static class ModuleInitializer
    {
        /// <summary>
        /// Initializes the module.
        /// </summary>
        public static void Initialize()
        {
            AssemblyManager.AddAssemblyToReview(typeof(PascalCaseSample));
            CodeReviewTestBase.Initialize();
        }
    }
}
