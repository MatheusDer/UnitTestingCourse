#nullable disable

using Bongo.Core.Services;
using Bongo.DataAccess.Repository.IRepository;
using Bongo.Models.Model;
using Bongo.Models.VM;
using Moq;
using NUnit.Framework;

namespace Bongo.Core.Test;

[TestFixture]
public class StudyRoomBookingServiceTests
{
    private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
    private Mock<IStudyRoomRepository> _studyRoomRepoMock;
    private StudyRoomBookingService _bookingService;
    private StudyRoomBooking _request;
    private List<StudyRoomBooking> _bookedRooms;
    private List<StudyRoom> _avaiableStudyRoom;

    public StudyRoomBookingServiceTests()
    {
        _request = new StudyRoomBooking
        {
            FirstName = "Ben",
            LastName = "Spark",
            Email = "test@test.com",
            Date = new DateTime(2023, 1, 1)
        };

        _bookedRooms = new List<StudyRoomBooking>
        {
            new StudyRoomBooking
            {
                StudyRoomId = 2,
                FirstName = "Ben2",
                LastName = "Spark2",
                Email = "test2@test.com",
                Date = new DateTime(2024, 1, 1)
            }
        };

        _avaiableStudyRoom = new List<StudyRoom>
        {
            new StudyRoom
            {
                Id = 1,
                RoomName = "Room",
                RoomNumber = "A1"
            }
        };
    }

    [SetUp]
    protected void SetUp()
    {
        _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
        _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
        _bookingService = new StudyRoomBookingService(_studyRoomBookingRepoMock.Object, _studyRoomRepoMock.Object);

        _studyRoomRepoMock.Setup(s => s.GetAll()).Returns(_avaiableStudyRoom);
        _studyRoomBookingRepoMock.Setup(s => s.GetAll(It.IsAny<DateTime>())).Returns(_bookedRooms);
    }

    [Test]
    public void GetAllBooking_CheckIfRepoIsCalled_WhenInvokeMethod()
    {
        _bookingService.GetAllBooking();
        _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
    }

    [Test]
    public void BookStudyRoom_ThrowsException_WhenNullRequest()
    {
        var exception = Assert.Throws<ArgumentNullException>(() => _bookingService.BookStudyRoom(null));
        Assert.AreEqual("request", exception.ParamName);
    }

    [Test]
    public void BookStudyRoom_ReturnsResultWithAllValues_WhenSaveBookingWithAvaiableRoom()
    {
        StudyRoomBooking savedStudyRoomBooking = null;
        _studyRoomBookingRepoMock.Setup(s => s.Book(It.IsAny<StudyRoomBooking>()))
            .Callback((StudyRoomBooking booking) => savedStudyRoomBooking = booking);

        _bookingService.BookStudyRoom(_request);

        _studyRoomBookingRepoMock.Verify(s => s.Book(It.IsAny<StudyRoomBooking>()), Times.Once);

        Assert.NotNull(savedStudyRoomBooking);
        Assert.AreEqual(savedStudyRoomBooking.FirstName, _request.FirstName);
        Assert.AreEqual(savedStudyRoomBooking.LastName, _request.LastName);
        Assert.AreEqual(savedStudyRoomBooking.Email, _request.Email);
        Assert.AreEqual(savedStudyRoomBooking.Date, _request.Date);
        Assert.AreEqual(_avaiableStudyRoom.First().Id, savedStudyRoomBooking.StudyRoomId);
    }

    [TestCase(0, false)]
    [TestCase(1, true)]
    public void BookStudyRoom_ValidateResultId(int expectedBookingId, bool roomAvailability)
    {
        if (!roomAvailability)
            _studyRoomRepoMock.Setup(s => s.GetAll()).Returns(new List<StudyRoom>());

        _studyRoomBookingRepoMock.Setup(s => s.Book(It.IsAny<StudyRoomBooking>()))
            .Callback((StudyRoomBooking booking) => booking.BookingId = expectedBookingId);

        var result = _bookingService.BookStudyRoom(_request);

        Assert.AreEqual(expectedBookingId, result.BookingId);
    }

    [Test]
    public void BookStudyRoom_RequestAndResultShouldMatch_WhenNoRoomAvailable()
    {
        _studyRoomRepoMock.Setup(s => s.GetAll()).Returns(new List<StudyRoom>());

        var result = _bookingService.BookStudyRoom(_request);

        Assert.NotNull(result);
        Assert.AreEqual(result.FirstName, _request.FirstName);
        Assert.AreEqual(result.LastName, _request.LastName);
        Assert.AreEqual(result.Email, _request.Email);
        Assert.AreEqual(result.Date, _request.Date);
    }

    [TestCase(true, ExpectedResult = StudyRoomBookingCode.Success)]
    [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvaiable)]
    public StudyRoomBookingCode BookStudyRoom_ValidateResultCode(bool isAnyRoomAvailable)
    {
        if (!isAnyRoomAvailable)
            _studyRoomRepoMock.Setup(s => s.GetAll()).Returns(new List<StudyRoom>());

        return _bookingService.BookStudyRoom(_request).Code;
    }

    [Test]
    public void BookStudyRoom_BookNotInvoked_WhenNoRoomAvailable()
    {
        _studyRoomRepoMock.Setup(s => s.GetAll()).Returns(new List<StudyRoom>());

        _bookingService.BookStudyRoom(_request);

        _studyRoomBookingRepoMock.Verify(s => s.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
    }
}
