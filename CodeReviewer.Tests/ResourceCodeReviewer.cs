using CodeReviewer.Engine;
using CodeReviewer.Engine.Reviewers.Resources;
using System;
using System.Text;

namespace CodeReviewer
{
    public class ResourceCodeReviewer
    {
        /// <summary>
        /// checkm resources of assembly code reviewer
        /// </summary>
        public void CheckResourceCodeReviewer()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var codeReviewer in CustomCodeReviewerManager.ResourceCodeReviewers)
            {
                foreach (var assembly in AssemblyManager.GetAssemblies())
                {
                    codeReviewer.Review(assembly, builder);
                }
            }
            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }

        /// <summary>
        /// check stream of assembly code reviewer
        /// </summary>
        public void CheckStreamCodeReviewer()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var codeReviewer in CustomCodeReviewerManager.StreamCodeReviewers)
            {
                foreach (var assembly in AssemblyManager.GetStreams())
                {
                    codeReviewer.Review(assembly, builder);
                }
            }
            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
