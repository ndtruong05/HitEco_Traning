using AnyCash.Configuration;
using System.Web.Mvc;

namespace AnyCash.Controllers
{
    public class NotifyController : Controller
    {
        [HttpGet, CompressFilter]
        public ActionResult Page404()
        {
            return View();
        }

        [HttpGet, CompressFilter]
        public ActionResult Error()
        {
            return View();
        }
    }
}