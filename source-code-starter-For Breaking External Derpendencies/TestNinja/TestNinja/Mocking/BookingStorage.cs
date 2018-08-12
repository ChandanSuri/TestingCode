using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class BookingStorage : IBookingStorage // With Booking Helper
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null) // we wanted this to be optional 
        {
            var unitOfWork = new UnitOfWork(); // External Database, so, External resource used...
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }
    }
}
