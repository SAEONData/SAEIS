using Microsoft.AspNetCore.Mvc;
using SAEIS.WebSite.Models;
using System.Diagnostics;

namespace SAEIS.WebSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("Acknowledgements")]
        public IActionResult Acknowledgements()
        {
            return View();
        }

        [Route("Background")]
        public IActionResult Background()
        {
            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("Links")]
        public IActionResult Links()
        {
            return View();
        }

        [Route("Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Researchers")]
        public IActionResult Researchers()
        {
            return View();
        }
    }
}