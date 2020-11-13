using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        //[AllowAnonymous]
        //public List<Estuary> Test()
        //{
        //    return dbContext.Estuaries.Include(i => i.Images).Take(10).ToList();
        //}

        public IActionResult Estuaries()
        {
            var model = dbContext.Estuaries.OrderBy(i => i.Name).Select(i => new EstuariesModel { Id = i.Id, Name = i.Name }).ToList();
            var rowNum = 1;
            model.ForEach(i => i.RowNum = rowNum++);
            SAEONLogs.Verbose("Model: {@Model}", model);
            return View(model);
        }

        public IActionResult EstuaryImages(int id)
        {
            var estauary = dbContext.Estuaries.Include(i => i.Images).FirstOrDefault(i => i.Id == id);
            if (estauary == null) return NotFound();
            var model = new ImagesModel
            {
                EstuaryId = estauary.Id,
                EstuaryName = estauary.Name,
                Images = estauary.Images.OrderBy(i => i.Type).ThenBy(i => i.SubType).ThenBy(i => i.Date).ThenBy(i => i.Name)
                .Select(i => new ImageModel
                {
                    Id = i.Id,
                    Type = i.Type,
                    SubType = i.SubType,
                    Date = i.Date,
                    Name = i.Name,
                    Source = i.Source,
                    Reference = i.Reference,
                    Notes = i.Notes
                }).ToList()
            };
            var rowNum = 1;
            model.Images.ForEach(i => i.RowNum = rowNum++);
            SAEONLogs.Verbose("Model: {@Model}", model);
            return View(model);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Images(ImagesModel model)
        //{
        //    model.Estuaries = dbContext.Estuaries.OrderBy(i => i.Name).Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToList();
        //    if (model.EstuaryId.HasValue)
        //    {
        //        var estuary = dbContext.Estuaries.Include(i => i.Images.Where(i => i.Available != "N")).First(i => i.Id == model.EstuaryId);
        //        var rowNum = 1;
        //        model.Images = estuary
        //            .Images
        //            .OrderBy(i => i.Type)
        //            .ThenBy(i => i.SubType)
        //            .ThenBy(i => i.Date)
        //            .ThenBy(i => i.Name)
        //            .Select(i =>
        //            new ImageModel
        //            {
        //                RowNum = rowNum++,
        //                Id = i.Id,
        //                Type = i.Type,
        //                SubType = i.SubType,
        //                Name = i.Name,
        //                LinkToImage = string.IsNullOrWhiteSpace(i.LinkToImage) || !i.LinkToImage.StartsWith("\\SAEDArchive\\") ? null : i.LinkToImage.Replace("SAEDArchive", "Archive"),
        //                LinkToHiResImage = i.LinkToHiResImage,
        //                HiResImageSize = i.HiResImageSize,
        //                Date = i.Date,
        //                Source = i.Source,
        //                Reference = i.Reference,
        //                Notes = i.Notes
        //            }).ToList();
        //    }
        //    SAEONLogs.Information("Model: {@Model}", model);
        //    return View(model);
        //}

    }
}
