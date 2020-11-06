using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using SAEIS.WebSite.Data;
using SAEIS.WebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace SAEIS.WebSite.Controllers
{
    [ResponseCache(Duration = 60 * 60 * 24 * 7)]
    public class MismatchController : Controller
    {
        private readonly IFileProvider _fileProvider;
        private readonly SAEISContext _dbContext = null;
        public MismatchController(IFileProvider fileProvider, SAEISContext dbContext)
        {
            _fileProvider = fileProvider;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            void GetFiles(string path, List<string> files)
            {
                foreach (var content in _fileProvider.GetDirectoryContents(path))
                {
                    if (!content.IsDirectory)
                    {
                        files.Add($"{path}/{content.Name}");
                    }
                    else
                    {
                        GetFiles($"{path}/{content.Name}", files);
                    }
                }
            }

            var model = new MismatchModel();
            // Literature
            // Folder
            var folderLiteratures = new List<string>();
            GetFiles("Archive/Literature", folderLiteratures);
            // Database
            var dbLiteratures = new List<string>();
            dbLiteratures.AddRange(_dbContext.Literatures.Where(i => !string.IsNullOrWhiteSpace(i.Link) && i.Link.StartsWith("\\SAEDArchive\\")).Select(i => i.Link.Replace(@"\", "/").Replace("/SAEDArchive/", "Archive/")));
            // Add folders missing from database
            model.Items.AddRange(folderLiteratures.Except(dbLiteratures).OrderBy(i => i).Select(i => new MismatchItemModel { Type = "Literature", FileName = i, InDirectory = true, InDatabase = false }));
            // In directory not in database
            model.Items.AddRange(dbLiteratures.Except(folderLiteratures).OrderBy(i => i).Select(i => new MismatchItemModel { Type = "Literature", FileName = i, InDirectory = false, InDatabase = true }));
            return View(model);
        }
    }
}