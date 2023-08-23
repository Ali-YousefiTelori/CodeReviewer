using CodeReviewer.Structures;
using System;
using System.IO;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.Resources
{
    public class StreamCodeReviewer : IReviewer<Stream>
    {
        Func<Stream, string> _checkIsValidFunc;
        public StreamCodeReviewer(Func<Stream, string> checkIsValidFunc)
        {
            _checkIsValidFunc = checkIsValidFunc;
        }

        public bool Review(Stream stream, StringBuilder builder)
        {
            bool hasError = false;
            var result = _checkIsValidFunc(stream);
            if (!string.IsNullOrEmpty(result))
            {
                hasError = true;
                builder.AppendLine(result);
            }
            return hasError;
        }
    }
}