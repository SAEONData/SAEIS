using Microsoft.AspNetCore.Mvc;
using SAEIS.Data;

namespace SAEIS.WebSite.Controllers
{
    public class InfoController : Controller
    {
        private SAEISDbContext dbContext = null;

        public InfoController(SAEISDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        ~InfoController()
        {
            dbContext = null;
        }

        //public IActionResult Index()
        //{
        //    return RedirectToAction("Index", "Search");
        //}

        [Route("Info/{id?}")]
        public IActionResult Index(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index", "Search");
            return View();
        }
    }
}