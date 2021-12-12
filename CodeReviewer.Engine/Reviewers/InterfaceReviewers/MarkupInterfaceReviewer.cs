using CodeReviewer.Engine;
using CodeReviewer.Structures;
using System;
using System.Text;

namespace CodeReviewer.Reviewers.InterfaceReviewers
{
    public class MarkupInterfaceReviewer : IReviewer<Type>
    {
        public bool Review(Type type, StringBuilder builder)
        {
            if (type.IsInterface)
            {
                if (type.GetPublicMethods().Count == 0 && type.GetPublicProperties().Count == 0)
                    builder.AppendLine($"AVOID using marker interfaces (interfaces with no members).! typeof {type.FullName}");
            }

            return false;
        }
    }
}
