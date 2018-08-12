using System;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public static class BookingHelper
    {
        // A DI Framework may not compile this type of Injection as Parameter, so, you have to change this to non-static
        public static string OverlappingBookingsExist(Booking booking, IBookingStorage bookingStorage)
        {
            if (booking.Status == "Cancelled")
                return string.Empty;

            var bookings = bookingStorage.GetActiveBookings(booking.Id);
            // We didn't get the logic below in a separate class because the implementation below is specific to the function here...
            var overlappingBooking =
                bookings.FirstOrDefault(
                    b =>
                        booking.ArrivalDate < b.DepartureDate &&
                        b.ArrivalDate < booking.DepartureDate);
            /*
             * booking.ArrivalDate >= b.ArrivalDate
               && booking.ArrivalDate < b.DepartureDate
               || booking.DepartureDate > b.ArrivalDate
               && booking.DepartureDate <= b.DepartureDate
               These tests don't work correctly...
             */

            return overlappingBooking == null ? string.Empty : overlappingBooking.Reference;
        }
    }

    public class UnitOfWork : IUnitOfWork
    {
        public IQueryable<T> Query<T>()
        {
            return new List<T>().AsQueryable();
        }
    }

    public class Booking
    {
        public string Status { get; set; }
        public int Id { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Reference { get; set; }
    }
}