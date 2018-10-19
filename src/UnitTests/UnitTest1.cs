using System;
using Core;
using Xunit;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var firstClass = new Class1();
            Assert.True(firstClass != null);
        }
    }
}
