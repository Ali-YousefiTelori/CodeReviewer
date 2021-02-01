using CodeReviewer.Engine;
using CodeReviewer.Reviewers.NamingConventions;
using System;
using System.Text;

namespace CodeReviewer.Tests
{
    internal class PropertiesReviewer
    {
        /// <summary>
        /// check validations of public properties
        /// </summary>
        public void PublicPropertiesOfClasses()
        {
            StringBuilder builder = new StringBuilder();

            PascalCodePropertyReviewer pascalCodePropertyReviewer = new PascalCodePropertyReviewer();
            foreach (var publicProperty in AssemblyManager.GetPublicProperties())
            {
                pascalCodePropertyReviewer.Review(publicProperty, builder);
            }

            if (builder.Length > 0)
                throw new Exception(builder.ToString());
        }
    }
}
