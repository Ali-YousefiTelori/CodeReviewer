using CodeReviewer.Reviewers.Customizations;
using CodeReviewer.Structures;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReviewer.Engine
{
    public static class CustomCodeReviewerManager
    {
        public static List<IReviewer> CustomCodeReviewer { get; } = new List<IReviewer>();
        public static void AddCustomTypeSuffixNamingCodeReviewer(string suffix, Func<Type, bool> checkTypeReviewerFunc)
        {
            CustomCodeReviewer.Add(new CustomTypeSuffixNamingCodeReviewer(suffix, checkTypeReviewerFunc));
        }
    }
}
