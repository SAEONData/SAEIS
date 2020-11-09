using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAEIS.WebSite.Data;
using SAEIS.WebSite.Models;
using SAEON.Logs;
using System.Linq;

namespace SAEIS.WebSite.Controllers
{
    [Authorize]
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
            SAEONLogs.Information("Model: {@Model}", model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Photos(PhotosModel model)
        {
            model.Estuaries = dbContext.Estuaries.OrderBy(i => i.Name).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList();
            if (model.EstuaryId.HasValue)
            {
                var estuary = dbContext.Estuaries.Include(i => i.EstuaryImages).ThenInclude(i => i.Image).First(i => i.Id == model.EstuaryId);
                var rowNum = 1;
                model.Images = estuary
                    .EstuaryImages.Select(i => i.Image).Where(i => i.Available != "N")
                    .OrderBy(i => i.Type)
                    .ThenBy(i => i.SubType)
                    .ThenBy(i => i.Date)
                    .ThenBy(i => i.Name)
                    .Select(i =>
                    new ImageModel
                    {
                        RowNum = rowNum++,
                        Id = i.Id,
                        Type = i.Type,
                        SubType = i.SubType,
                        Name = i.Name,
                        LinkToImage = string.IsNullOrWhiteSpace(i.LinkToImage) || !i.LinkToImage.StartsWith("\\SAEDArchive\\") ? null : i.LinkToImage.Replace("SAEDArchive", "Archive"),
                        LinkToHiResImage = i.LinkToHiResImage,
                        HiResImageSize = i.HiResImageSize,
                        Date = i.Date,
                        Source = i.Source,
                        Reference = i.Reference,
                        Notes = i.Notes
                    }).ToList();
            }
            SAEONLogs.Information("Model: {@Model}", model);
            return View(model);
        }

    }
}
