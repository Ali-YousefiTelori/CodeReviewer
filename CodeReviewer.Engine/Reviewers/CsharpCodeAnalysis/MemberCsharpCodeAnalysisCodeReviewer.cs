using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Text;

namespace CodeReviewer.Engine.Reviewers.CsharpCodeAnalysis
{
    public class MemberCsharpCodeAnalysisCodeReviewer : CsharpCodeAnalysisBaseReviewer
    {
        Func<MemberDeclarationSyntax, (string Prefix, string Suffix)> _checkIsValidFunc;
        public MemberCsharpCodeAnalysisCodeReviewer(Func<MemberDeclarationSyntax, (string Prefix, string Suffix)> checkIsValidFunc)
        {
            if (checkIsValidFunc == null)
                throw new ArgumentNullException(nameof(checkIsValidFunc));
            _checkIsValidFunc = checkIsValidFunc;
        }

        public override bool Review(ClassDeclarationSyntax reviewData, StringBuilder builder)
        {
            bool hasError = false;
            foreach (var member in reviewData.Members)
            {
                var result = _checkIsValidFunc(member);
                if (string.IsNullOrEmpty(result.Suffix) && string.IsNullOrEmpty(result.Prefix))
                    continue;
                var className = reviewData.Identifier.ValueText;
                var namespaceDeclaration = reviewData.Parent as BaseNamespaceDeclarationSyntax;
                var namespaceName = namespaceDeclaration?.Name.ToString() ?? "";

                builder.AppendLine($"{result.Prefix} {namespaceName}.{className} Of member [{member}] {result.Suffix}");
                hasError = true;
            }
            return hasError;
        }
    }
}
