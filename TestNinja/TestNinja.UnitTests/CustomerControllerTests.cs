using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    // For Testing the Return Type of Methods, in case of non-primitive objects made...
    [TestFixture]
    class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();
            var result = controller.GetCustomer(0);

            // Here the first one is better...
            // Not Found Type only
            Assert.That(result, Is.TypeOf<NotFound>());
            // Not Found or one of its derivatives
            Assert.That(result, Is.InstanceOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();
            var result = controller.GetCustomer(2);

            // Not Found Type only
            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
