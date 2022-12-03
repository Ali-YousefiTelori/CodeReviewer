using CodeReviewer.Engine;
using CodeReviewer.Engine.Reviewers.Customizations;
using CodeReviewer.Reviewers.Customizations;
using CodeReviewer.Structures;
using System;
using System.Linq;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class CustomCodeReviewer
    {
        void CheckCustomReviewer(Func<IReviewer, bool> func, Func<Type, bool> typeFunc = null)
        {
            if (typeFunc == null)
                typeFunc = x => true;
            StringBuilder builder = new StringBuilder();
            foreach (var reviewer in CustomCodeReviewerManager.CustomCodeReviewer.Where(x => func(x)))
            {
                foreach (Type type in AssemblyManager.GetPublicTypes().Where(typeFunc))
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
        public void CheckCustomTypeSuffixNamingCodeReviewer()
        {
            CheckCustomReviewer(x => x is CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer && reviewer.IsSuffix);
        }

        /// <summary>
        /// check custom type code reviewer
        /// </summary>
        public void CheckCustomTypePrefixNamingCodeReviewer()
        {
            CheckCustomReviewer(x => x is CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer && !reviewer.IsSuffix);
        }

        /// <summary>
        /// check enum custom type code reviewer
        /// </summary>
        public void CheckCustomEnumValuesCodeReviewer()
        {
            CheckCustomReviewer(x => x is CustomEnumValuesCodeReviewer reviewer, x => x.IsEnum);
        }

        /// <summary>
        /// check fast custom type code reviewer
        /// </summary>
        public void CheckFastCustomCodeReviewer()
        {
            CheckCustomReviewer(x => x is FastCustomCodeReviewer reviewer);
        }
    }
}
