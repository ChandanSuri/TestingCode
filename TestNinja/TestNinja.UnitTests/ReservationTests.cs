using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestClass]
    public class ReservationTests
    {
        [TestMethod]
        public void CanBeCancelledBy_AdminCancelling_ReturnsTrue()// MethodName_Scenario_Behaviour
        {
            // Arrange -> Initialization of the objects, creating instances and other things to be used...
            var reservationObj = new Reservation();

            // Act
            var result = reservationObj.CanBeCancelledBy(new User { IsAdmin = true });

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void CanBeCancelledBy_SameUserCancelling_ReturnsTrue()
        {
            var user = new User();
            var reservationObj = new Reservation { MadeBy = user };

            var result = reservationObj.CanBeCancelledBy(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CanBeCancelledBy_AnotherUserCancelling_ReturnsFalse()
        {
            var reservationObj = new Reservation { MadeBy = new User() };

            var result = reservationObj.CanBeCancelledBy(new User());

            Assert.IsFalse(result);
        }
    }
}
