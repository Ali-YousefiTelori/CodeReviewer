# CodeReviewer
CodeReview will help you to review your c# codes XUnit with test cases


simple usage:

```csharp
    public class CodeReviewerTests : MainCodeReviewer
    {
        static CodeReviewerTests()
        {
            //types to check (this will check all of types in assembly so no need to add all of types of assembly)
            AssemblyManager.AddAssemblyToReview(typeof(PascalCaseSample));
        }
    }
```
