using Microsoft.AspNetCore.Mvc;

namespace AppGiros.Controllers
{
    public class CiudadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
