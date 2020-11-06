using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SAEIS.WebSite.Areas.Identity.Data;
using SAEIS.WebSite.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SAEIS.WebSite.Controllers
{
    [ResponseCache(Duration = 60 * 60 * 24 * 7)]
    public class HomeController : Controller
    {
        private readonly SignInManager<SAEISIdentityUser> signInManager;

        public HomeController(SignInManager<SAEISIdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

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

        [Route("HowToCite")]
        public IActionResult HowToCite()
        {
            return View();
        }

        [Route("Links")]
        public IActionResult Links()
        {
            return View();
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await signInManager.SignOutAsync();
            return View("Index");
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