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
            foreach (CustomTypeSuffixNamingCodeReviewer reviewer in CustomCodeReviewerManager.CustomCodeReviewer.Where(x => x is CustomTypeSuffixNamingCodeReviewer))
            {
                foreach (var type in AssemblyManager.GetPublicTypes())
                {
                    reviewer.Review(type, builder);
                }
            }
            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
