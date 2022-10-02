using NUnit.Framework;
using Moq;
using Bongo.Core.Services.IServices;
using Bongo.Web.Controllers;
using Bongo.Models.Model;
using Bongo.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace Bongo.Web.Test;

[TestFixture]
public class RoomBookingControllerTests
{
    private Mock<IStudyRoomBookingService> _studyRommBookingService;
    private RoomBookingController _roomBookingController;

    [SetUp]
    public void SetUp()
    {
        _studyRommBookingService = new Mock<IStudyRoomBookingService>();
        _roomBookingController = new RoomBookingController(_studyRommBookingService.Object);
    }

    [Test]
    public void IndexPage_InvokeGetAllOnce_WhenRequest()
    {
        _roomBookingController.Index();

        _studyRommBookingService.Verify(s => s.GetAllBooking(), Times.Once);
    }

    [Test]
    public void BookPage_ReturnToView_WhenModelInvalid()
    {
        _roomBookingController.ModelState.AddModelError("test", "test");

        var result = _roomBookingController.Book(new StudyRoomBooking());

        var viewResult = (ViewResult)result;

        Assert.AreEqual("Book", viewResult.ViewName);
    }

    [Test]
    public void BookPage_SetError_WhenNoRoomAvaiable()
    {
        _studyRommBookingService.Setup(s => s.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns(new StudyRoomBookingResult() { Code = StudyRoomBookingCode.NoRoomAvaiable });

        var result = _roomBookingController.Book(new StudyRoomBooking()) as ViewResult;

        Assert.AreEqual("No Study Room avaiable for selected date", result!.ViewData["Error"]);
    }

    [Test]
    public void BookPage_Redirects_WhenRoomAvaiable()
    {
        var studyBookingRoom = new StudyRoomBooking()
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "test@test.com",
            Date = DateTime.Now,
        };

        _studyRommBookingService.Setup(s => s.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
            .Returns(new StudyRoomBookingResult()
            {
                FirstName = studyBookingRoom.FirstName,
                LastName = studyBookingRoom.LastName,
                Email = studyBookingRoom.Email,
                Date = studyBookingRoom.Date,
                Code = StudyRoomBookingCode.Success
            });

        var result = _roomBookingController.Book(studyBookingRoom) as RedirectToActionResult;

        Assert.AreEqual("BookingConfirmation", result!.ActionName);
        Assert.AreEqual(studyBookingRoom.FirstName, result.RouteValues!["FirstName"]);
        Assert.AreEqual(studyBookingRoom.LastName, result.RouteValues!["LastName"]);
        Assert.AreEqual(studyBookingRoom.Email, result.RouteValues!["Email"]);
        Assert.AreEqual(studyBookingRoom.Date, result.RouteValues!["Date"]);
        Assert.AreEqual(StudyRoomBookingCode.Success, result.RouteValues!["Code"]);
    }
}
