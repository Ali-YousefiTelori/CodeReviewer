namespace CodeReviewer.Samples
{
    public enum InvalidGender : byte
    {
        Male = 0,
        Female = 1
    }

    public enum Gender : byte
    {
        None = 0,
        Male = 1,
        Female = 2
    }

    public class CustomTypeSample
    {
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public bool Passport { get; set; }
        public bool HasPassport2 { get; set; }
        public bool IsMale { get; set; }

        public bool Valid()
        {
            return false;
        }
    }

}

namespace CodeReviewer.Contract.Common
{
    public class CodeReviewerCommonContract
    {

    }
}
