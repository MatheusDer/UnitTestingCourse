using Bongo.DataAccess.Repository;
using Bongo.Models.Model;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Bongo.DataAccess.Test;

[TestFixture]
public class StudyRoomBookingRepositoryTests
{
    private StudyRoomBooking studyRoomBooking_One;
    private StudyRoomBooking studyRoomBooking_Two;

    public DbContextOptions<AppDbContext> options { get; private set; }

    public StudyRoomBookingRepositoryTests()
    {
        studyRoomBooking_One = new()
        {
            FirstName = "Ben1",
            LastName = "Spark1",
            Date = new DateTime(2023, 1, 1),
            Email = "test@email.com",
            BookingId = 11,
            StudyRoomId = 1
        };
        studyRoomBooking_Two = new()
        {
            FirstName = "Ben2",
            LastName = "Spark2",
            Date = new DateTime(2023, 2, 2),
            Email = "test@email.com",
            BookingId = 22,
            StudyRoomId = 2
        };
    }

    [SetUp]
    protected void SetUp()
    {
        options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("InMem").Options;
    }

    [TearDown]
    protected void TearDown()
    {
        using var context = new AppDbContext(options);
        context.Database.EnsureDeleted(); 
    }

    [Test]
    public void SaveBooking_CheckDatabaseValues_WhenBookingExists()
    {
        var expectedResult = new List<StudyRoomBooking> { studyRoomBooking_One, studyRoomBooking_Two };

        using var context = new AppDbContext(options);

        var repository = new StudyRoomBookingRepository(context);
        repository.Book(studyRoomBooking_One);
        repository.Book(studyRoomBooking_Two);

        var bookingsFromDb = repository.GetAll(null);

        Assert.NotNull(bookingsFromDb);
        Assert.AreEqual(expectedResult, bookingsFromDb);
    }

    [Test]
    public void SaveBooking_CheckDatabaseValues_WhenDontExists()
    {
        using var context = new AppDbContext(options);

        var repository = new StudyRoomBookingRepository(context);
        repository.Book(studyRoomBooking_One);

        var bookingFromDb = context.StudyRoomBookings.FirstOrDefault(s => s.BookingId == studyRoomBooking_Two.BookingId);

        Assert.Null(bookingFromDb);
        Assert.AreNotEqual(studyRoomBooking_Two, bookingFromDb);
    }
}
