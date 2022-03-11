namespace CodeReviewer.Structures
{
    using System;

    [Flags]
    public enum CheckType
    {
        None = 0,
        All = TypeName | PropertyName | FieldName | MethodName | MethodParameterName | ConstructorParameterName,
        TypeName = 2,
        PropertyName = 4,
        FieldName = 8,
        MethodName = 16,
        MethodParameterName = 32,
        ConstructorParameterName = 64
    }
}
