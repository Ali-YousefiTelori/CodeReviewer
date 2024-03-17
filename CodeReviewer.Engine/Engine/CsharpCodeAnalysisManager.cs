using CodeReviewer.Engine.Reviewers.CsharpCodeAnalysis;
using CodeReviewer.Structures;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;

namespace CodeReviewer.Engine
{
    public class CsharpCodeAnalysisManager
    {
        public static Dictionary<IReviewer, IEnumerable<ClassDeclarationSyntax>> CsharpCodeAnalysisReviewers { get; } = new Dictionary<IReviewer, IEnumerable<ClassDeclarationSyntax>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="classes"></param>
        /// <param name="checkIsValidFunc"></param>
        public static void AddMemberCsharpCodeAnalysisCodeReviewer(IEnumerable<ClassDeclarationSyntax> classes, Func<ClassDeclarationSyntax, MemberDeclarationSyntax, (string Prefix, string Suffix, bool IsHandled)> checkIsValidFunc)
        {
            MemberCsharpCodeAnalysisCodeReviewer reviewer = new MemberCsharpCodeAnalysisCodeReviewer(checkIsValidFunc);
            CsharpCodeAnalysisReviewers.Add(reviewer, classes);
        }
    }
}