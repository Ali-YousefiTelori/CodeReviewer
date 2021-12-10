using CodeReviewer.Engine;
using CodeReviewer.Reviewers.NamingConventions;
using System;
using System.Reflection;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class PropertiesReviewer
    {
        public static bool IsThrowInverse { get; set; }
        /// <summary>
        /// check validations of public properties
        /// </summary>
        public void PublicPropertiesOfClassesReview()
        {
            StringBuilder builder = new StringBuilder();

            PascalCodePropertyReviewer pascalCodePropertyReviewer = new PascalCodePropertyReviewer();
            foreach (PropertyInfo publicProperty in AssemblyManager.GetPublicProperties())
            {
                pascalCodePropertyReviewer.Review(publicProperty, builder);
            }


            if (!IsThrowInverse && builder.Length > 0)
                throw new Exception(builder.ToString());
            if (IsThrowInverse && builder.Length == 0)
                throw new Exception("There is no error found!");
        }
    }
}
