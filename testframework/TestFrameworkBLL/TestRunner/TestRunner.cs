using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TestFrameWorkCore.Assertion;
using TestFrameWorkCore.Attributes;

namespace TestFrameworkBLL.TestRunner
{
    public class TestRunner
    {
        public void Run(Assembly? testAssembly)
        {
            var testClasses = testAssembly.GetTypes().Where(type => type.IsClass &&
                                                          Attribute.GetCustomAttribute(type, typeof(BaseTestFixture)) is not null);

            if (testClasses.Any())
            {
                foreach (var testClass in testClasses)
                {
                    GetTestMethods(testClass);
                }
            }
            else
            {
                TestResult(false, "there are no classes to test.", null);
            }
        }

        private void GetTestMethods(Type testClass)
        {

            var classMethods = testClass.GetMethods();
            var setUp = classMethods.
                Where(method => Attribute.GetCustomAttribute(method, typeof(SetUpAttribute)) is not null);
            var tearDown = classMethods.
                Where(method => Attribute.GetCustomAttribute(method, typeof(TearDownAttribute)) is not null);
            var testMethods = classMethods.
                Where(method => Attribute.GetCustomAttribute(method, typeof(TestCaseAttribute)) is not null);
            
            if (testMethods.Any())
            {
                foreach (var testMethod in testMethods)
                {
                    RunTests(testMethod, setUp, tearDown, testClass);
                }
            }
            else
            {
                TestResult(false, "there are no methods to test.", null);
            }
        }

        public void RunTests(MethodInfo testMethod, IEnumerable<MethodInfo> setUpMethods,
            IEnumerable<MethodInfo> tearDownMethods, object testClass)
        {
            
            foreach (var setUpMethod in setUpMethods)
            {
                setUpMethod.Invoke(null, null);
                Console.WriteLine();
            }   
            RunTest(testMethod, testClass);
            
            foreach (var tearDownMethod in tearDownMethods)
            {
                tearDownMethod.Invoke(testClass, null);
                Console.WriteLine("\n");
            } 
            
        }


        public void RunTest(MethodInfo method, object testClass)
        {

            var isSuccessfull = true;
            string? testMessage = null;
            var methodName = method.Name;

            try
            {
                method.Invoke(testClass, null);

            }
            catch (AssertionException ex)
            {
                isSuccessfull = false;
                testMessage = ex.Message;
            }
            catch (Exception ex)
            {
                isSuccessfull = false;
                testMessage = ex.Message;
            }
            TestResult(isSuccessfull, testMessage, methodName);
        }

        private void TestResult(bool isSuccesfull, string? testMessage, string? methodName)
        {
                if (isSuccesfull)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Test passed: {methodName} \n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine($"Test failed: {methodName} \n{testMessage} \n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                
        }

    }

}
