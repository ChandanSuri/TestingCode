using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinjaUnitTests.Mocking
{
    [TestFixture]
    class BookingHelper_OverlappingBookingExistTests
    {
        private Booking _booking;
        private Mock<IBookingStorage> _storage;

        [SetUp]
        public void SetUp()
        {
            // Existing Bookings in DB
            _booking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = "a"
            };

            _storage = new Mock<IBookingStorage>();

            _storage.Setup(s => s.GetActiveBookings(1)).Returns(new List<Booking>
            {
                _booking
            }.AsQueryable());

        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate, days : 5),
                DepartureDate = Before(_booking.ArrivalDate)
            }, _storage.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.ArrivalDate)
            }, _storage.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = Before(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate)
            }, _storage.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = Before(_booking.DepartureDate)
            }, _storage.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnExistingBookingAndFinishesAfterIt_ReturnExistingBookingsReference()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate)
            }, _storage.Object);

            Assert.That(result, Is.EqualTo(_booking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.DepartureDate),
                DepartureDate = After(_booking.DepartureDate, days: 3)
            }, _storage.Object);

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = BookingHelper.OverlappingBookingsExist(new Booking
            {
                Id = 1,
                ArrivalDate = After(_booking.ArrivalDate),
                DepartureDate = After(_booking.DepartureDate, days: 2),
                Status = "Cancelled"
            }, _storage.Object);

            Assert.That(result, Is.Empty);
        }

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int date)
        {
            return new DateTime(year, month, date, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int date)
        {
            return new DateTime(year, month, date, 10, 0, 0);
        }
    }
}
