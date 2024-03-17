﻿using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Linq;
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

                builder.AppendLine($"{result.Prefix} {namespaceName}.{className} Of member [{GetText(member)}] {result.Suffix}");
                hasError = true;
            }
            return hasError;
        }

        string GetText(MemberDeclarationSyntax member)
        {
            var memberText = GetNotEmptyMonth(member.GetText()?.Lines);
            var text = memberText?.ToString();
            if (string.IsNullOrEmpty(text))
                return member.ToString().Trim();
            return text.ToString();
        }

        string GetNotEmptyMonth(TextLineCollection textLines)
        {
            if (textLines == null)
                return null;
            for (int i = 0; i < textLines.Count; i++)
            {
                var text = textLines?.Skip(i)?.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(text?.ToString()))
                    return text.ToString().Trim();
            }
            return null;
        }
    }
}
