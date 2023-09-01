using CodeReviewer.Reviewers.Customizations;
using System;
using System.IO.Compression;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.Customizations
{
    public class FastCustomCodeReviewer : CustomCodeBaseReviewer<Type>
    {
        Func<Type, (string Prefix, string Suffix)> _checkIsValidFunc;
        public FastCustomCodeReviewer(Func<Type, (string Prefix, string Suffix)> checkIsValidFunc)
        {
            if (checkIsValidFunc == null)
                throw new ArgumentNullException(nameof(checkIsValidFunc));
            _checkIsValidFunc = checkIsValidFunc;
        }

        public override bool Review(Type reviewData, StringBuilder builder)
        {
            try
            {
                var result = _checkIsValidFunc(reviewData);
                if (string.IsNullOrEmpty(result.Suffix) && string.IsNullOrEmpty(result.Prefix))
                    return false;
                builder.AppendLine($"{result.Prefix} {reviewData.FullName} {result.Suffix}");
                return true;
            }
            catch (Exception)
            {

            }
            return default;
        }
    }
}
