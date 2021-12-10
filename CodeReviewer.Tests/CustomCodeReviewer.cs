using CodeReviewer.Engine;
using CodeReviewer.Reviewers.Customizations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class CustomCodeReviewer
    {
        /// <summary>
        /// check custom type code reviewer
        /// </summary>
        public void CheckCustomTypeSuffixNamingCodeReviewer()
        {
            StringBuilder builder = new StringBuilder();
            foreach (CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer in CustomCodeReviewerManager.CustomCodeReviewer.Where(x => x is CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer && reviewer.IsSuffix))
            {
                foreach (Type type in AssemblyManager.GetPublicTypes())
                {
                    reviewer.Review(type, builder);
                }
            }
            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }

        /// <summary>
        /// check custom type code reviewer
        /// </summary>
        public void CheckCustomTypePrefixNamingCodeReviewer()
        {
            StringBuilder builder = new StringBuilder();
            foreach (CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer in CustomCodeReviewerManager.CustomCodeReviewer.Where(x => x is CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer && !reviewer.IsSuffix))
            {
                foreach (Type type in AssemblyManager.GetPublicTypes())
                {
                    reviewer.Review(type, builder);
                }
            }
            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
