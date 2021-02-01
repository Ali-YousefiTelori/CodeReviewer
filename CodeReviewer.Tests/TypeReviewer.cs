using CodeReviewer.Engine;
using CodeReviewer.Reviewers.NamingConventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class TypeReviewer
    {
        /// <summary>
        /// check validations of public types
        /// </summary>
        public void PublicTypesReview()
        {
            StringBuilder builder = new StringBuilder();

            PascalCodeTypeReviewer pascalCodeTypeReviewer = new PascalCodeTypeReviewer();
            foreach (var type in AssemblyManager.GetPublicTypes())
            {
                pascalCodeTypeReviewer.Review(type, builder);
            }

            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
