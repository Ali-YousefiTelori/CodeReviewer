using CodeReviewer.Engine;
using CodeReviewer.Structures;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CodeReviewer.Reviewers.Customizations
{
    /// <summary>
    /// check suffix text naming of custom types
    /// </summary>
    public class CustomTypeSuffixAndPrefixNamingCodeReviewer : CustomCodeBaseReviewer<Type>
    {
        public bool IsSuffix { get; set; }
        public StringComparison StringComparison { get; set; }
        string[] _acceptedSuffixes;
        string[] _acceptedPrefixes;
        CheckType _checkType;
        Func<Type, bool> _checkTypeReviewerFunc;
        Func<Type, bool> _checkInsideOfTypeReviewerFunc;

        /// <summary>
        /// check suffix text naming of custom types
        /// </summary>
        /// <param name="acceptedSufixes">suffixes name you want to be end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this suffix</param>
        /// <param name="checkInsideOfTypeReviewerFunc">everytypes inside of the main type come here, return true for any type you want check this suffix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public void InitializeSuffix(Func<Type, bool> checkTypeReviewerFunc, Func<Type, bool> checkInsideOfTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, string[] acceptedSufixes)
        {
            if (acceptedSufixes == null || acceptedSufixes.Length == 0 || acceptedSufixes.Any(x => string.IsNullOrEmpty(x)))
                throw new Exception("suffixes cannot be null or empty!");
            else if (checkTypeReviewerFunc == null)
                throw new Exception("checkTypeReviewerFunc cannot be null!");
            IsSuffix = true;
            _acceptedSuffixes = acceptedSufixes;
            _checkTypeReviewerFunc = checkTypeReviewerFunc;
            _checkInsideOfTypeReviewerFunc = checkInsideOfTypeReviewerFunc;
            _checkType = checkType;
            StringComparison = stringComparison;
        }

        /// <summary>
        /// check prefix text naming of custom types
        /// </summary>
        /// <param name="acceptedSufixes">prefixes name you want to be end of type name conventions</param>
        /// <param name="checkTypeReviewerFunc">everytypes come here, return true for any type you want check this prefix</param>
        /// <param name="checkInsideOfTypeReviewerFunc">everytypes inside of the main type come here, return true for any type you want check this prefix</param>
        /// <param name="checkType">types of you want to check in c#</param>
        public void InitializePrefix(Func<Type, bool> checkTypeReviewerFunc, Func<Type, bool> checkInsideOfTypeReviewerFunc, CheckType checkType, StringComparison stringComparison, string[] acceptedPrefixes)
        {
            if (acceptedPrefixes == null || acceptedPrefixes.Length == 0 || acceptedPrefixes.Any(x => string.IsNullOrEmpty(x)))
                throw new Exception("prefixes cannot be null or empty!");
            else if (checkTypeReviewerFunc == null)
                throw new Exception("checkTypeReviewerFunc cannot be null!");
            _acceptedPrefixes = acceptedPrefixes;
            _checkTypeReviewerFunc = checkTypeReviewerFunc;
            _checkInsideOfTypeReviewerFunc = checkInsideOfTypeReviewerFunc;
            _checkType = checkType;
            StringComparison = stringComparison;
        }

        public override bool Review(Type reviewData, StringBuilder builder)
        {
            if (_checkTypeReviewerFunc(reviewData))
            {
                bool hasError = false;
                bool hasClass = (_checkType & CheckType.TypeName) == CheckType.TypeName;
                if (hasClass && CheckHasErrorsOnSuffixOrPrefix(reviewData.Name, out string[] errorCaptions))
                {
                    builder.AppendLine($"Type of \"{reviewData.FullName}\" has not used {string.Join(" or ", errorCaptions)}");
                    hasError = true;
                }

                if (_checkInsideOfTypeReviewerFunc != null)
                {
                    bool hasProperty = (_checkType & CheckType.PropertyName) == CheckType.PropertyName;
                    if (hasProperty)
                    {
                        foreach (PropertyInfo property in reviewData.GetPublicProperties())
                        {
                            if (_checkInsideOfTypeReviewerFunc(property.PropertyType) && CheckHasErrorsOnSuffixOrPrefix(property.Name, out errorCaptions))
                            {
                                builder.AppendLine($"Type of \"{reviewData.FullName}\" with Property name of \"{property.Name}\" has not used {string.Join(" or ", errorCaptions)}");
                                hasError = true;
                            }
                        }
                    }

                    bool hasField = (_checkType & CheckType.FieldName) == CheckType.FieldName;
                    if (hasField)
                    {
                        foreach (FieldInfo field in reviewData.GetRuntimeFields())
                        {
                            if (!field.GetCustomAttributes().Any(att => att is CompilerGeneratedAttribute) && _checkInsideOfTypeReviewerFunc(field.FieldType) && CheckHasErrorsOnSuffixOrPrefix(field.Name, out errorCaptions))
                            {
                                builder.AppendLine($"Type of \"{reviewData.FullName}\" with Field name of \"{field.Name}\" has not used {string.Join(" or ", errorCaptions)}");
                                hasError = true;
                            }
                        }
                    }

                    bool hasMethod = (_checkType & CheckType.MethodName) == CheckType.MethodName;
                    if (hasMethod)
                    {
                        foreach (MethodInfo method in reviewData.GetPublicMethods())
                        {
                            if (_checkInsideOfTypeReviewerFunc(method.ReturnType) && CheckHasErrorsOnSuffixOrPrefix(method.Name, out errorCaptions))
                            {
                                builder.AppendLine($"Type of \"{reviewData.FullName}\" with Method name of \"{method.Name}\" has not used {string.Join(" or ", errorCaptions)}");
                                hasError = true;
                            }
                        }
                    }
                }

                return hasError;
            }
            return false;
        }

        bool CheckHasErrorsOnSuffixOrPrefix(string name, out string[] errorCaptions)
        {
            errorCaptions = null;
            if (_acceptedSuffixes?.Length > 0)
            {
                if (!_acceptedSuffixes.Any(x => name.EndsWith(x, StringComparison)))
                {
                    errorCaptions = _acceptedSuffixes.Select(suffix => $"suffix of \"{suffix}\" you have to change it to \"{name + suffix}\"").ToArray();
                }
            }
            if (_acceptedPrefixes?.Length > 0)
            {
                if (!_acceptedPrefixes.Any(x => name.StartsWith(x, StringComparison)))
                {
                    errorCaptions = _acceptedPrefixes.Select(prefix => $"prefix of \"{prefix}\" you have to change it to \"{prefix + name}\"").ToArray();
                }
            }

            return errorCaptions != null;
        }
    }
}
