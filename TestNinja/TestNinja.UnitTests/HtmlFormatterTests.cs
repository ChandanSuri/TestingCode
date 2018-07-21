using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // Testing Strings
    [TestFixture]
    class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("abc");

            // Specific Test - Assertion (too specific)
            // But for this case this might work, but for eg, if you have some error statement returned
            // Then you may want a little general assertion to test
            Assert.That(result, Is.EqualTo("<strong>abc</strong>"));

            // For ignoring case...
            // Assert.That(result, Is.EqualTo("<strong>abc</strong>").IgnoreCase);

            // Too General, it shouldn't be too general as well...
            Assert.That(result, Does.StartWith("<strong>"));
            Assert.That(result, Does.EndWith("</strong>"));
            // The above two assertions may make it a less too general, thus, making it perfect for cases like errors...
            // Although the above 2 statements collectively are fine, we can still make it better...
            Assert.That(result, Does.Contain("abc"));
        }
    }
}
