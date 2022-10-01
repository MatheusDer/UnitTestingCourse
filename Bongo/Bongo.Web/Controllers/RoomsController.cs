using Bongo.Core.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Bongo.Web.Controllers;

public class RoomsController : Controller
{
    private readonly IStudyRoomService _service;

    public RoomsController(IStudyRoomService service)
    {
        _service = service;
    }

    public IActionResult Index()
    {
        return View(_service.GetAll());
    }
}
