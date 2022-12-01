using CodeReviewer.Reviewers.Customizations;
using System;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.Customizations
{
    /// <summary>
    /// Customize check of your enums
    /// </summary>
    /// <typeparam name="T">type of Enum to check</typeparam>
    public class CustomEnumValuesCodeReviewer : CustomCodeBaseReviewer<Type>
    {
        Func<(string Name, int Index, object Value), bool> _checkEnumNameAndValueReviewerFunc;
        /// <summary>
        /// Initialize check enum name and values
        /// </summary>
        /// <param name="checkEnumNameAndValueReviewerFunc">function to check enum name and values, if you return true it means true</param>
        /// <exception cref="Exception"></exception>
        public void Initialize(Func<(string Name, int Index, object Value), bool> checkEnumNameAndValueReviewerFunc)
        {
            if (checkEnumNameAndValueReviewerFunc == null)
                throw new Exception("checkEnumNameAndValueReviewerFunc cannot be null!");
            _checkEnumNameAndValueReviewerFunc = checkEnumNameAndValueReviewerFunc;
        }

        public override bool Review(Type reviewData, StringBuilder builder)
        {
            if (!reviewData.IsEnum)
                return true;
            var names = Enum.GetNames(reviewData);
            var values = Enum.GetValues(reviewData);
            bool hasError = false;
            for (int i = 0; i < names.Length; i++)
            {
                var value = Convert.ToInt64(values.GetValue(i));
                if (!_checkEnumNameAndValueReviewerFunc((names[i], i, value)))
                {
                    builder.AppendLine($"Enum type of \"{reviewData.FullName}\" with name of \"{names[i]}\" and index of {i} with value {value} is not valid!");
                    hasError = true;
                }
            }
            return !hasError;
        }
    }
}
