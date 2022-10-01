using Bongo.Core.Services.IServices;
using Bongo.Models.Model;
using Bongo.Models.VM;
using Microsoft.AspNetCore.Mvc;

namespace Bongo.Web.Controllers;

public class RoomBookingController : Controller
{
    private readonly IStudyRoomBookingService _studyRoomBookingService;

    public RoomBookingController(IStudyRoomBookingService studyRoomBookingService)
    {
        _studyRoomBookingService = studyRoomBookingService;
    }

    public IActionResult Index()
        => View(_studyRoomBookingService.GetAllBooking());

    public IActionResult Book()
        => View();

    [HttpPost]
    public IActionResult Book(StudyRoomBooking studyRoomBooking)
    {
        if (ModelState.IsValid)
        {
            var result = _studyRoomBookingService.BookStudyRoom(studyRoomBooking);
            if (result.Code == StudyRoomBookingCode.Success)
                return RedirectToAction("BookingConfirmation", result);

            ViewData["Error"] = "No Study Room avaiable for selected date";
        }

        return View("Book");
    }

    public IActionResult BookingConfirmation(StudyRoomBookingResult result)
        => View(result);
}
