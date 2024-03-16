using CodeReviewer.Structures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.CsharpCodeAnalysis
{
    public abstract class CsharpCodeAnalysisBaseReviewer : IReviewer<ClassDeclarationSyntax>, IReviewer
    {
        public abstract bool Review(ClassDeclarationSyntax reviewData, StringBuilder builder);

        public bool Review(object reviewData, StringBuilder builder)
        {
            return Review((ClassDeclarationSyntax)reviewData, builder);
        }
    }
}
