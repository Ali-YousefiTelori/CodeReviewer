using CodeReviewer.Engine.Reviewers.Customizations;
using CodeReviewer.Engine.Reviewers.Resources;
using CodeReviewer.Reviewers.Customizations;
using CodeReviewer.Structures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace CodeReviewer.Engine
{
    public static class CustomCodeReviewerManager
    {
        public static List<IReviewer> CustomCodeReviewers { get; } = new List<IReviewer>();
        public static List<IReviewer<Assembly>> ResourceCodeReviewers { get; } = new List<IReviewer<Assembly>>();
        public static List<IReviewer<Stream>> StreamCodeReviewers { get; } = new List<IReviewer<Stream>>();
        internal static List<Type> SkippedTypesCodeReviewers { get; } = new List<Type>();

        /// <summary>
        /// check suffix text naming of custom types
        /// </summary>
        /// <param name="suffixes">suffixes names you want to be in end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this suffix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public static void AddCustomTypeSuffixNamingCodeReviewer(Func<Type, bool> checkTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, params string[] suffixes)
        {
            CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer = new CustomTypeSuffixAndPrefixNamingCodeReviewer();
            reviewer.InitializeSuffix(checkTypeReviewerFunc, null, checkType, stringComparison, suffixes);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// check suffix text naming of custom types
        /// </summary>
        /// <param name="suffixes">suffixes names you want to be in end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this suffix</param>
        /// <param name="checkInsideOfTypeReviewerFunc">everytypes inside of the main type come here, return true for any type you want check this suffix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public static void AddCustomInsideOfTypeSuffixNamingCodeReviewer(Func<Type, bool> checkTypeReviewerFunc, Func<Type, bool> checkInsideOfTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, params string[] suffixes)
        {
            CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer = new CustomTypeSuffixAndPrefixNamingCodeReviewer();
            reviewer.InitializeSuffix(checkTypeReviewerFunc, checkInsideOfTypeReviewerFunc, checkType, stringComparison, suffixes);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// check prefix text naming of custom types
        /// </summary>
        /// <param name="prefixes">prefixes names you want to be in end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this prefix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public static void AddCustomTypePrefixNamingCodeReviewer(Func<Type, bool> checkTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, params string[] prefixes)
        {
            CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer = new CustomTypeSuffixAndPrefixNamingCodeReviewer();
            reviewer.InitializePrefix(checkTypeReviewerFunc, null, checkType, stringComparison, prefixes);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// check prefix text naming of custom types
        /// </summary>
        /// <param name="prefixes">prefixes names you want to be in end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this prefix</param>
        /// <param name="checkInsideOfTypeReviewerFunc">everytypes inside of the main type come here, return true for any type you want check this prefix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public static void AddCustomInsideOfTypePrefixNamingCodeReviewer(Func<Type, bool> checkTypeReviewerFunc, Func<Type, bool> checkInsideOfTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, params string[] prefixes)
        {
            CustomTypeSuffixAndPrefixNamingCodeReviewer reviewer = new CustomTypeSuffixAndPrefixNamingCodeReviewer();
            reviewer.InitializePrefix(checkTypeReviewerFunc, checkInsideOfTypeReviewerFunc, checkType, stringComparison, prefixes);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// Add custom reviewer for your enums types
        /// </summary>
        /// <param name="checkEnumNameAndValueReviewerFunc"></param>
        public static void AddCustomEnumValuesCodeReviewer(Func<(string Name, int Index, object Value), bool> checkEnumNameAndValueReviewerFunc)
        {
            CustomEnumValuesCodeReviewer reviewer = new CustomEnumValuesCodeReviewer();
            reviewer.Initialize(checkEnumNameAndValueReviewerFunc);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// Add a fast custom reviewer to fast implement a custom reviewer
        /// </summary>
        /// <param name="checkIsValidFunc"></param>
        public static void AddFastCustomCodeReviewer(Func<Type, (string Prefix, string Suffix)> checkIsValidFunc)
        {
            FastCustomCodeReviewer reviewer = new FastCustomCodeReviewer(checkIsValidFunc);
            CustomCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// Add an assembly resource reviewer
        /// </summary>
        /// <param name="checkIsValidFunc"></param>
        public static void AddResourceCustomCodeReviewer(Func<string, Stream, string> checkIsValidFunc)
        {
            ResourceCodeReviewer reviewer = new ResourceCodeReviewer(checkIsValidFunc);
            ResourceCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// Add a stream resource reviewer
        /// </summary>
        /// <param name="checkIsValidFunc"></param>
        public static void AddStreamCustomCodeReviewer(Func<Stream, string> checkIsValidFunc)
        {
            StreamCodeReviewer reviewer = new StreamCodeReviewer(checkIsValidFunc);
            StreamCodeReviewers.Add(reviewer);
        }

        /// <summary>
        /// skip this type to review
        /// </summary>
        /// <param name="types">This type will skip to review</param>
        public static void AddTypeToSkipCodeReviewer(params Type[] types)
        {
            SkippedTypesCodeReviewers.AddRange(types);
        }
    }
}
