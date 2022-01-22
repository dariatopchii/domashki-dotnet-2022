namespace TestFrameWorkCore.Assertion
{
    public static class Assert
    {

        public static void AreEqual(object expected, object actual)
        {
            if (!expected.Equals(actual))
            {
                throw new AssertionException($"The actual parameter is not equal to the expected one. " +
                                             $"\nExpected {expected}, got {actual}");
            }  
        }
        
        
    }
}