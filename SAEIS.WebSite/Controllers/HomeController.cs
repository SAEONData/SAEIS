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

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Acknowledgements()
        {
            return View();
        }

        public IActionResult Background()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Links()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Researchers()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View();
        }
    }
}