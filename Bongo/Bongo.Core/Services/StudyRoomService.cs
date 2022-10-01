using Bongo.Core.Services.IServices;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;

namespace Bongo.Core.Services;

public class StudyRoomService : IStudyRoomService
{
	private readonly IStudyRoomRepository _repository;

	public StudyRoomService(IStudyRoomRepository repository)
	{
		_repository = repository;
	}

	public IEnumerable<StudyRoom> GetAll()
		=> _repository.GetAll();
}
