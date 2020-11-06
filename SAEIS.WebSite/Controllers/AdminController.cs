using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SAEIS.WebSite.Data;
using SAEIS.WebSite.Models;
using System.Linq;

namespace SAEIS.WebSite.Controllers
{
    public class AdminController : Controller
    {
        private readonly SAEISContext dbContext;

        public AdminController(SAEISContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Photos()
        {
            var model = new PhotosModel
            {
                Estuaries = dbContext.Estuaries.OrderBy(i => i.Name).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(PhotosModel model)
        {
            return View(model);
        }

    }
}
