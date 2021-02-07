using CodeReviewer.Engine;
using CodeReviewer.Reviewers.NamingConventions;
using System;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class MethodsReviewer
    {
        /// <summary>
        /// check validations of public properties
        /// </summary>
        public void PublicMethodsOfClassesReview()
        {
            StringBuilder builder = new StringBuilder();

            PascalCodeMethodReviewer pascalCodePropertyReviewer = new PascalCodeMethodReviewer();
            foreach (var publicMethod in AssemblyManager.GetMethodsOfProperties())
            {
                pascalCodePropertyReviewer.Review(publicMethod, builder);
            }

            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
