using CodeReviewer.Engine;
using CodeReviewer.Reviewers.InterfaceReviewers;
using System;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class InterfaceReviewer
    {
        public void MarkupInterfaceReview()
        {
            StringBuilder builder = new StringBuilder();

            MarkupInterfaceReviewer pascalCodePropertyReviewer = new MarkupInterfaceReviewer();
            foreach (Type type in AssemblyManager.GetPublicTypes())
            {
                pascalCodePropertyReviewer.Review(type, builder);
            }

            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
