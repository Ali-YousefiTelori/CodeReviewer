using CodeReviewer.Structures;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.Resources
{
    public class ResourceCodeReviewer : IReviewer<Assembly>
    {
        Func<string, Stream, string> _checkIsValidFunc;
        public ResourceCodeReviewer(Func<string, Stream, string> checkIsValidFunc)
        {
            _checkIsValidFunc = checkIsValidFunc;
        }

        public bool Review(Assembly assembly, StringBuilder builder)
        {
            bool hasError = false;
            foreach (var resourceName in assembly.GetManifestResourceNames())
            {
                var resource = assembly.GetManifestResourceStream(resourceName);
                var result = _checkIsValidFunc(resourceName, resource);
                if (!string.IsNullOrEmpty(result))
                {
                    hasError = true;
                    builder.AppendLine(result);
                }
            }
            return hasError;
        }
    }
}
