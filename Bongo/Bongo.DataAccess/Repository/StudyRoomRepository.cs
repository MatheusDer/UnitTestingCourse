using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;

namespace Bongo.DataAccess.Repository;

public class StudyRoomRepository : IStudyRoomRepository
{
	private readonly AppDbContext _context;

	public StudyRoomRepository(AppDbContext context)
	{
		_context = context;
	}

	public IEnumerable<StudyRoom> GetAll()
		=> _context.StudyRooms.ToList();
}
