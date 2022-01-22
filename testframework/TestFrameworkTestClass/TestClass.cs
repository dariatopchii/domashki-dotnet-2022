using System;
using TestFrameWorkCore.Assertion;
using TestFrameWorkCore.Attributes;

namespace TestFrameworkTestClass
{
    
    [BaseTestFixture]
    public class TestClass
    {
        
        [SetUp]
        public static void SetUp()
        {
            Console.WriteLine("setup");
        }
        
        [TearDown]
        public static void TearDown()
        {
            Console.WriteLine("teardown");
        }
       
        [TestCase]
        public static void Addition()
        {
            int a = 0;
            int b = 0;
            Assert.AreEqual(a, b);
        }

        [TestCase]
        public static void WrongResultAddition()
        {
            int a = 0;
            int b = 1;
            Assert.AreEqual(a, b);
        }

    }
}

