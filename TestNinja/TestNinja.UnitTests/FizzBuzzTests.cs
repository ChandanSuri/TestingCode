using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // Exercise 1
    [TestFixture]
    class FizzBuzzTests
    {
        // Since here the function is static, we dont need to create and object (Arrange)
        [Test]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnsFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy3Only_ReturnsFizz()
        {
            var result = FizzBuzz.GetOutput(3);
            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy5Only_ReturnsBuzz()
        {
            var result = FizzBuzz.GetOutput(5);
            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_InputIsNotDivisibleBy3Or5_ReturnsTheSameNumber()
        {
            var result = FizzBuzz.GetOutput(1);
            Assert.That(result, Is.EqualTo("1"));
        }
    }
}
