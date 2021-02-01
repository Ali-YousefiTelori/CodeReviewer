using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CodeReview.Tests
{
    public abstract class MainCodeReviewer
    {
        #region Properties Reviewer

        [Fact]
        public void PropertiesReview() => new PropertiesReviewer().PublicPropertiesOfClasses();

        #endregion
    }
}
