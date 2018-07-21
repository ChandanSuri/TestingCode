using System;
using TestNinja.Fundamentals;
using NUnit.Framework;

// Here NUnit is used and not MUnit
namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests2
    {
        [Test]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()// MethodName_Scenario_Behaviour
        {
            // Arrange -> Initialization of the objects, creating instances and other things to be used...
            var reservationObj = new Reservation();

            // Act
            var result = reservationObj.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            //Assert.IsTrue(result);
            // 2 more ways
            Assert.That(result, Is.True);
            //Assert.That(result == true);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            var user = new User();
            var reservationObj = new Reservation { MadeBy = user };

            var result = reservationObj.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            var reservationObj = new Reservation { MadeBy = new User() };

            var result = reservationObj.CanBeCancelledBy(new User());

            Assert.IsFalse(result);
        }
    }
}
