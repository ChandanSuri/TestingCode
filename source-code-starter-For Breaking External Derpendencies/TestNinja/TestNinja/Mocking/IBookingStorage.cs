using System.Linq;

namespace TestNinja.Mocking
{
    public interface IBookingStorage
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }
}