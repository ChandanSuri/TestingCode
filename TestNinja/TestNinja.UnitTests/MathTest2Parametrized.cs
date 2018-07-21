using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    class MathTest2Parametrized
    {
        private Math _math;
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        // For ignoring some tests at times...
        [Test]
        [Ignore("Because I wanted to! Hehe...")]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            var result = _math.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }

        // to provide parameters TestCase is used...
        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        // For Testing Arrays and Collections, neither making it too specific nor too general.
        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnOddNumbersUptoLimit()
        {
            var result = _math.GetOddNumbers(5);

            // Too General
            Assert.That(result, Is.Not.Empty);
            // To make it a little more specific, but still it's too general.
            Assert.That(result.Count(), Is.EqualTo(3));

            // We can also check for some elements that should be present in the array, could include corner cases...
            // But we shouldn't care for the order, if the collection is not sorted and will remain that way.
            Assert.That(result, Does.Contain(1));
            Assert.That(result, Does.Contain(3));
            Assert.That(result, Does.Contain(5));

            // The above 2 statements can be combined in one as...
            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 })); // Neither too general, nor too specific...

            // Some Useful Assertions =>
            // To Check if the collection is ordered... (sorted)
            Assert.That(result, Is.Ordered);
            // To check of the array or collection contains all unique elements...
            Assert.That(result, Is.Unique);
            // Don't use all the assertions at a time.
        }
    }
}
