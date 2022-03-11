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
                if (type.GetMembers().Length == 0)
                    builder.AppendLine($"AVOID using marker interfaces (interfaces with no members).! typeof {type.FullName}");
            }

            return false;
        }
    }
}
