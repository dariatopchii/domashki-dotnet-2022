using System;

namespace TestFrameWorkCore.Assertion
{
    public class AssertionException: Exception
    {
        public AssertionException(string exeptionmessage): base(exeptionmessage)
        {
            Console.WriteLine(exeptionmessage);
        }
    }
}