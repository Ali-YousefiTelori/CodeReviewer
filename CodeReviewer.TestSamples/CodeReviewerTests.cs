using CodeReviewer.Engine;
using CodeReviewer.Samples;
using CodeReviewer.Structures;
using CodeReviewer.Tests;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.IO;
using System.Linq;
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
            //check a resouce file
            CustomCodeReviewerManager.AddResourceCustomCodeReviewer((string resourceName, Stream resource) =>
            {
                using var reader = new StreamReader(resource);
                {
                    var text = reader.ReadToEnd();
                    if (text.Contains("ConnectionString"))
                        return "ConnectionString detected";
                    return null;
                }
            });
            MemoryStream memoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(memoryStream);
            writer.Write("ConnectionString");
            writer.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);
            AssemblyManager.AddStreamsToReview(memoryStream);
            //check a resouce file
            CustomCodeReviewerManager.AddStreamCustomCodeReviewer((Stream resource) =>
            {
                using var reader = new StreamReader(resource);
                {
                    var text = reader.ReadToEnd();
                    if (text.Contains("ConnectionString"))
                        return "ConnectionString detected";
                    return null;
                }
            });



            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            var parentDir = Path.GetFullPath(Path.Combine(currentDir, @"..\"));
            var grandparentDir = Path.GetFullPath(Path.Combine(parentDir, @"..\"));
            var greatGrandparentDir = Path.GetFullPath(Path.Combine(grandparentDir, @"..\"));
            var greatGrandGrandparentDir = Path.GetFullPath(Path.Combine(greatGrandparentDir, @"..\"));
            var filePath = Path.Combine(greatGrandGrandparentDir, "CodeReviewer.Samples", "CodeAnalysisExample.cs");
            //"CodeAnalysisExample.cs"
            string sourceCode = File.ReadAllText(filePath);

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

            var root = syntaxTree.GetRoot();

            var classDeclarations = root.DescendantNodes().OfType<ClassDeclarationSyntax>();


            CsharpCodeAnalysisManager.AddMemberCsharpCodeAnalysisCodeReviewer(classDeclarations, x =>
            {
                if (x.Kind() == SyntaxKind.FieldDeclaration)
                {
                    if (string.IsNullOrEmpty(x.Modifiers.FirstOrDefault().Text))
                        return ("Class", "Has no access modifier", false);
                }
                return default;
            });
        }

        #region Pascal Code Reviewer

        [Fact]
        public override void PropertiesReview()
        {
            try
            {
                base.PropertiesReview();
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
            }
            catch (Exception ex)
            {
                Assert.Contains("Type of \"CodeReviewer.Samples.CustomTypeSample\" with Property name of \"Passport\" has not used prefix of \"Has\" you have to change it to \"HasPassport\" or prefix of \"Have\" you have to change it to \"HavePassport\" or prefix of \"Is\" you have to change it to \"IsPassport\" or prefix of \"Can\" you have to change it to \"CanPassport\"\r\nType of \"CodeReviewer.Samples.CustomTypeSample\" with Method name of \"Valid\" has not used prefix of \"Has\" you have to change it to \"HasValid\" or prefix of \"Have\" you have to change it to \"HaveValid\" or prefix of \"Is\" you have to change it to \"IsValid\" or prefix of \"Can\" you have to change it to \"CanValid\"", ex.Message);
            }
        }

        [Fact]
        public override void FastCustomCodeReview()
        {
            try
            {
                base.FastCustomCodeReview();
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
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
                Assert.Fail("no message detected!");
            }
            catch (Exception ex)
            {
                Assert.StartsWith("Enum type of \"CodeReviewer.Samples.InvalidGender\" with name of \"Male\" and index of 0 with value 0 is not valid!", ex.Message);
            }
        }

        #endregion

        #region Resources

        [Fact]
        public override void ResourcesReview()
        {
            try
            {
                base.ResourcesReview();
                Assert.Fail("no message detected!");
            }
            catch (Exception ex)
            {
                Assert.StartsWith("ConnectionString detected", ex.Message);
            }
        }

        [Fact]
        public override void StreamReview()
        {
            try
            {
                base.StreamReview();
                Assert.Fail("no message detected!");
            }
            catch (Exception ex)
            {
                Assert.StartsWith("ConnectionString detected", ex.Message);
            }
        }

        #endregion

        public override void CsharpCodeAnalysisReview()
        {
            try
            {
                base.CsharpCodeAnalysisReview();
                Assert.Fail("no message detected!");
            }
            catch (Exception ex)
            {
                Assert.EndsWith("Has no access modifier\r\n", ex.Message);
            }
        }
    }
}
