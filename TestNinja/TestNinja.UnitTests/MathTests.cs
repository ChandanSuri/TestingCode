using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathTests
    {
        private Math _math;
        // NUnit has the two capabilities to having the user not initialize many things 
        // everytime a test has to be written for a class, that introduces redundancy
        // But the tests should not depend on each other as well, along with it should also
        // not depend on a common argument initialized once because of state leakage,
        // which NUnit takes care of...
        // The two phases are: SetUp(to initialize, called before each test) and,
        // TearDown(to do cleanup, called after each test)

        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }

        [Test]
        public void Add_WhenCalled_ReturnTheSumOfArguments()
        {
            //var math = new Math();
            var result = _math.Add(1, 2);
            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        public void Max_FirstArgumentIsGreater_ReturnTheFirstArgument()
        {
            //var math = new Math();
            var result = _math.Max(2, 1);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_SecondArgumentIsGreater_ReturnTheSecondArgument()
        {
            //var math = new Math();
            var result = _math.Max(1, 2);
            Assert.That(result, Is.EqualTo(2));
        }

        [Test]
        public void Max_ArgumentsAreEqual_ReturnsTheSameArgument()
        {
            var math = new Math();
            var result = math.Max(2, 2);
            Assert.That(result, Is.EqualTo(2));
        }
    }
}
