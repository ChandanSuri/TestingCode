using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class ErrorLoggerTests
    {
        // For Testing void methods
        // Whch change some state of an object and does some action, so we need to check those parameters
        // because here nothing is returned, we need to check those changes and persistent states...
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        // For Testing methods that throw exceptions
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();
            // logger.log(error); This will give an error when called, so, we need to use a delegate for this...
            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }

        // Testing methods that raise an event, mostly used for WPF and Xamarin applications...
        // Here a new Guid is made with a diferent Id for it.
        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var logger = new ErrorLogger();
            var id = Guid.Empty;

            // Below an event raised from a source (sender) and with some parameters (args) is specified with the body {}
            // Here we would like to get the id of the new Guid formed as a result of raising the event.
            // Subscribe to that event before acting, and then have some parameters filled up to ensure after action that
            // the event was raised safely.
            logger.ErrorLogged += (sender, args) => { id = args; };

            // Acting
            logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }

        // NOTE for PRIVATE/PROTECTED FUNCTIONS/MEMBERS
        // You should never test private/protected members because they are the application's implementation and,
        // If you change the implementation, then the tests can break and thus, will come in your way thus,
        // wasting your time and resources. So, never test them because they are tightly coupled with the implementation
        // details. Check the public ones because they are the direct knobs to your application and which also use
        // the private and protected members, thus, giving an implicit test for them as well, but don't test the private
        // and protected members directly (writing test cases for them)... NEVER!!!!!!!!!
    }
}
