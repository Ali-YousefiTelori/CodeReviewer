using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CodeReview.Tests
{
    public static class CodeReviewTestBase
    {
        static TaskCompletionSource<bool> TaskCompletionSource { get; set; } = new TaskCompletionSource<bool>();
        public static Task WaitForInitialization()
        {
            return TaskCompletionSource.Task;
        }

        public static void Initialize()
        {
            TaskCompletionSource.SetResult(true);
        }
    }
}
