using System;
using System.Text;

namespace CodeReviewer.Reviewers.Customizations
{
    public class CustomTypeSuffixNamingCodeReviewer : CustomCodeBaseReviewer<Type>
    {
        string _suffix;
        Func<Type, bool> _checkTypeReviewerFunc;
        public CustomTypeSuffixNamingCodeReviewer(string suffix, Func<Type, bool> checkTypeReviewerFunc)
        {
            if (string.IsNullOrEmpty(suffix))
                throw new Exception("suffix cannot be null or empty!");
            else if (checkTypeReviewerFunc == null)
                throw new Exception("checkTypeReviewerFunc cannot be null!");
            _suffix = suffix;
            _checkTypeReviewerFunc = checkTypeReviewerFunc;
        }

        public override bool Review(Type reviewData, StringBuilder builder)
        {
            if (_checkTypeReviewerFunc(reviewData))
            {
                if (!reviewData.Name.EndsWith(_suffix))
                {
                    builder.AppendLine($"Type of {reviewData.FullName} has not used suffix of {_suffix} you have to change it to {reviewData.Name + _suffix}");
                    return true;
                }
            }
            return false;
        }
    }
}
