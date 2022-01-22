using System;

namespace TestFrameWorkCore.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TearDownAttribute: Attribute
    {
    }
}