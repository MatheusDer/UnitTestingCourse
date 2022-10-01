using Bongo.Core.Services.IServices;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.VM;

namespace Bongo.Core.Services;

public class StudyRoomBookingService : IStudyRoomBookingService
{
	private readonly IStudyRoomBookingRepository _studyRoomBookingRepository;
	private readonly IStudyRoomRepository _studyRoomRepository;

	public StudyRoomBookingService
	(
		IStudyRoomBookingRepository studyRoomBookingRepository,
		IStudyRoomRepository studyRoomRepository
	)
	{
		_studyRoomBookingRepository = studyRoomBookingRepository;
		_studyRoomRepository = studyRoomRepository;
	}

	public StudyRoomBookingResult BookStudyRoom(StudyRoomBooking request)
	{
		if (request is null)
			throw new ArgumentNullException(nameof(request));

		var result = new StudyRoomBookingResult
		{
			FirstName = request.FirstName,
			LastName = request.LastName,
			Email = request.Email,
			Date = request.Date,
		};

		var bookedRooms = _studyRoomBookingRepository
			.GetAll(request.Date)
			.Select(s => s.StudyRoomId);

		var avaiableRooms = _studyRoomRepository
			.GetAll()
			.Where(s => !bookedRooms.Contains(s.Id));

		if (avaiableRooms.Any())
		{
			var studyRoomBooking = new StudyRoomBooking
			{
				FirstName = request.FirstName,
				LastName = request.LastName,
				Email = request.Email,
				Date = request.Date,
				StudyRoomId = avaiableRooms
				.First().Id
			};

			_studyRoomBookingRepository.Book(studyRoomBooking);

			result.BookingId = studyRoomBooking.BookingId;
			result.Code = StudyRoomBookingCode.Success;

			return result;
		}

		result.Code = StudyRoomBookingCode.NoRoomAvaiable;
		return result;
	}

	public IEnumerable<StudyRoomBooking> GetAllBooking()
		=> _studyRoomBookingRepository.GetAll(null);
}
