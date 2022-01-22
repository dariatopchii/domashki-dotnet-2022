using System;
using System.Reflection;
using TestFrameworkBLL.TestRunner;

namespace PL
{

    public static class Run
    {
        public static void Main()
        {
            Console.Clear();
            Assembly testAssembly = Assembly.Load(AssemblyName.GetAssemblyName(@"TestFrameworkTestClass.dll").ToString());
            if (!testAssembly.Equals(null))
            {
                var testRunner = new TestRunner();
                testRunner.Run(testAssembly);
            }
        }

       
    }
}