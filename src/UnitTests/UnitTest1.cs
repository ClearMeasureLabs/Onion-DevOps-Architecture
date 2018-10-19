using System;
using Core;
using NUnit.Framework;

namespace UnitTests
{
    public class Class1Tests
    {
        [Test]
        public void ShouldCreateClass()
        {
            var firstClass = new Class1();
            Assert.True(firstClass != null);
        }
    }
}
