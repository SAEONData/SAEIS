using Microsoft.AspNetCore.Mvc;
using SAEIS.Data;
using SAEIS.WebSite.Models;
using System.Linq;

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
            var estuary = dbContext.Estuaries.FirstOrDefault(i => i.Id == id.Value);
            if (estuary == null) return RedirectToAction("Index", "Search");
            var model = new InfoViewModel
            {
                Estuary = estuary
            };
            return View(model);
        }
    }
}