using CodeReviewer.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReviewer
{
    internal class CsharpCodeAnalysisReviewer
    {
        /// <summary>
        /// check validations of public properties
        /// </summary>
        public void CsharpCodeAnalysisReview()
        {
            StringBuilder builder = new StringBuilder();

            foreach (var review in CsharpCodeAnalysisManager.CsharpCodeAnalysisReviewers)
            {
                foreach (var item in review.Value)
                {
                    review.Key.Review(item, builder);
                }
            }

            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
