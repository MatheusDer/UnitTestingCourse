using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;

namespace Bongo.DataAccess.Repository;

public class StudyRoomBookingRepository : IStudyRoomBookingRepository
{
	private readonly AppDbContext _context;

	public StudyRoomBookingRepository(AppDbContext context)
	{
		_context = context;
	}

	public IEnumerable<StudyRoomBooking> GetAll(DateTime? date)
	{
		if (date is not null)
			return _context.StudyRoomBookings
				.Where(s => s.Date == date)
				.OrderBy(s => s.BookingId)
				.ToList();

		return _context.StudyRoomBookings
			.OrderBy(s => s.BookingId)
			.ToList();
	}

	public void Book(StudyRoomBooking booking)
	{
		_context.StudyRoomBookings.Add(booking);
		_context.SaveChanges();
	}
}
