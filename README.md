# CodeReviewer
CodeReview will help you to review your c# codes XUnit with test cases


simple usage:

```csharp
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            //types to check (this will check all of types in assembly so no need to add all of types of assembly)
            AssemblyManager.AddAssemblyToReview(typeof(pascalCaseSample));
            //enum types to check has "Type" suffix like GenderType
            CustomCodeReviewerManager.AddCustomTypeSuffixNamingCodeReviewer(x => x.IsEnum, CheckType.TypeName, System.StringComparison.Ordinal, "Type");
            //check properties and methods and fields that has "Type" suffix like GetGenderType()
            CustomCodeReviewerManager.AddCustomInsideOfTypeSuffixNamingCodeReviewer(x => x.IsClass, x => x.IsEnum, CheckType.PropertyName | CheckType.MethodName | CheckType.FieldName, System.StringComparison.Ordinal, "Type");
            //check properties and methods and fields that has some boolean suffix like HasPassport
            CustomCodeReviewerManager.AddCustomInsideOfTypePrefixNamingCodeReviewer(x => x.IsClass, x => x == typeof(bool), CheckType.PropertyName | CheckType.MethodName | CheckType.FieldName, System.StringComparison.Ordinal, "Has", "Have", "Is", "Can");
        }
    }
```
