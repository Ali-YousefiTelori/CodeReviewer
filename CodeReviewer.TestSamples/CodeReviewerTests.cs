using CodeReviewer.Engine;
using CodeReviewer.Samples;
using CodeReviewer.Structures;
using CodeReviewer.Tests;
using System;
using Xunit;

namespace CodeReviewer.TestSamples
{
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            //types to check (this will check all of types in assembly so no need to add all of types of assembly)
            AssemblyManager.AddAssemblyToReview(typeof(CustomTypeSample));
            //enum types to check has "Type" suffix like GenderType
            CustomCodeReviewerManager.AddCustomTypeSuffixNamingCodeReviewer(x => x.IsEnum, CheckType.TypeName, System.StringComparison.Ordinal, "Type");
            //check properties and methods and fields that has "Type" suffix like GetGenderType()
            CustomCodeReviewerManager.AddCustomInsideOfTypeSuffixNamingCodeReviewer(x => x.IsClass, x => x.IsEnum, CheckType.PropertyName | CheckType.MethodName | CheckType.FieldName, System.StringComparison.Ordinal, "Type");
            //check properties and methods and fields that has some boolean suffix like HasPassport
            CustomCodeReviewerManager.AddCustomInsideOfTypePrefixNamingCodeReviewer(x => x.IsClass, x => x == typeof(bool), CheckType.PropertyName | CheckType.MethodName | CheckType.FieldName, System.StringComparison.Ordinal, "Has", "Have", "Is", "Can");
            //check names and values and indexes of enum
            CustomCodeReviewerManager.AddCustomEnumValuesCodeReviewer(((string Name, int Index, object Value) Data) => Data.Index != 0 || Data.Name == "None");
            //check a type details very fast with a reviewer
            CustomCodeReviewerManager.AddFastCustomCodeReviewer(type => type.Namespace.Contains(".Contract.") ? ("NameSpace of type", "is not a valid namespace!") : default);
        }

        #region Pascal Code Reviewer

        [Fact]
        public override void PropertiesReview()
        {
            try
            {
                base.PropertiesReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Pascal case of Property of type CodeReviewer.Samples.pascalCaseSample with name myPropertyName is not a valid pascal case naming conventions!", ex.Message);
            }
        }

        [Fact]
        public override void TypePascalCodeReview()
        {
            try
            {
                base.TypePascalCodeReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("You must add 'I' to start name of interface of CodeReviewer.Samples.Myinterface", ex.Message);
            }
        }

        [Fact]
        public override void MethodPascalCodeReview()
        {
            try
            {
                base.MethodPascalCodeReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Pascal case of Method of type CodeReviewer.Samples.pascalCaseSample with name myMethod is not a valid pascal case naming conventions!", ex.Message);
                Assert.EndsWith("Camel case of Method Parameter of type CodeReviewer.Samples.pascalCaseSample of parameter type of System.Int32 with name MyAge is not a valid camel case naming conventions!", ex.Message.TrimEnd());
            }
        }

        #endregion

        #region Custom Code Reviewer

        [Fact]
        public override void SuffixCodeReview()
        {
            try
            {
                base.SuffixCodeReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Type of \"CodeReviewer.Samples.InvalidGender\" has not used suffix of \"Type\" you have to change it to \"InvalidGenderType\"\r\nType of \"CodeReviewer.Samples.Gender\" has not used suffix of \"Type\" you have to change it to \"GenderType\"", ex.Message);
                Assert.Contains("Type of \"CodeReviewer.Samples.Gender\" has not used suffix of \"Type\" you have to change it to \"GenderType\"", ex.Message);
                Assert.EndsWith("Type of \"CodeReviewer.Samples.CustomTypeSample\" with Property name of \"Gender\" has not used suffix of \"Type\" you have to change it to \"GenderType\"", ex.Message.TrimEnd());
            }
        }

        [Fact]
        public override void PrefixCodeReview()
        {
            try
            {
                base.PrefixCodeReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Type of \"CodeReviewer.Samples.CustomTypeSample\" with Property name of \"Passport\" has not used prefix of \"Has\" you have to change it to \"HasPassport\" or prefix of \"Have\" you have to change it to \"HavePassport\" or prefix of \"Is\" you have to change it to \"IsPassport\" or prefix of \"Can\" you have to change it to \"CanPassport\"\r\nType of \"CodeReviewer.Samples.CustomTypeSample\" with Method name of \"Valid\" has not used prefix of \"Has\" you have to change it to \"HasValid\" or prefix of \"Have\" you have to change it to \"HaveValid\" or prefix of \"Is\" you have to change it to \"IsValid\" or prefix of \"Can\" you have to change it to \"CanValid\"", ex.Message);
            }
        }

        [Fact]
        public override void FastCustomCodeReview()
        {
            try
            {
                base.FastCustomCodeReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("NameSpace of type CodeReviewer.Contract.Common.CodeReviewerCommonContract is not a valid namespace!", ex.Message);
            }
        }

        #endregion

        #region Interface

        [Fact]
        public override void MarkupInterfaceReview()
        {
            try
            {
                base.MarkupInterfaceReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("AVOID using marker interfaces (interfaces with no members).! typeof CodeReviewer.Samples.IInterfaceSamples\r\nAVOID using marker interfaces (interfaces with no members).! typeof CodeReviewer.Samples.Iinterface\r\nAVOID using marker interfaces (interfaces with no members).! typeof CodeReviewer.Samples.Myinterface", ex.Message);
            }
        }

        #endregion

        #region Enums

        [Fact]
        public override void EnumsReview()
        {
            try
            {
                base.EnumsReview();
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Enum type of \"CodeReviewer.Samples.InvalidGender\" with name of \"Male\" and index of 0 with value 0 is not valid!", ex.Message);
            }
        }

        #endregion
    }
}
