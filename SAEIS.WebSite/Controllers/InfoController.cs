using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAEIS.Data;
using SAEIS.WebSite.Models;
using SAEON.Logs;
using System;
using System.Collections.Generic;
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

        public class ListItem
        {
            public string Id { get; set; } = null;
            public string Text { get; set; }
            public bool IsSelected { get; set; }
        }



        [Route("Info/{id?}")]
        public IActionResult Index(int? id)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id } }))
            {
                try
                {
                    SelectList SelectListFrom(IQueryable<ListItem> list)
                    {
                        var newList = list.OrderBy(i => i.Text).ToList();
                        newList.Insert(0, new ListItem { Text = "All", IsSelected = true });
                        return new SelectList(newList, "Id", "Text", "All");
                    }

                    if (!id.HasValue)
                    {
                        return RedirectToAction("Index", "Search");
                    }

                    var estuary = dbContext.Estuaries
                        .Include(i => i.Classification)
                        .Include(i => i.Condition)
                        .Include(i => i.Region)
                        .Include(i => i.Geomorphology)
                        .Include(i => i.InfoAvailable)
                        .Include(i => i.WaterQuality)
                        .Include(i => i.ManagementClassification)
                        .Include(i => i.SanctuaryProtection)
                        .Include(i => i.UndevelopedMargin)
                        .Include(i => i.WaterRequirement)
                        .Include(i => i.PriorityForRehabilitation)
                        .FirstOrDefault(i => i.Id == id.Value);
                    if (estuary == null)
                    {
                        return RedirectToAction("Index", "Search");
                    }
                    var model = new InfoViewModel
                    {
                        Estuary = estuary,
                        LiteratureTypes = SelectListFrom(dbContext.Literatures.Where(i => !string.IsNullOrWhiteSpace(i.Type)).Select(i => new ListItem { Id = i.Type, Text = i.Type }).Distinct()),
                        LiteratureSubTypes = SelectListFrom(dbContext.Literatures.Where(i => !string.IsNullOrWhiteSpace(i.SubType)).Select(i => new ListItem { Id = i.Type, Text = i.SubType }).Distinct()),
                        MapTypes = SelectListFrom(dbContext.Maps.Where(i => !string.IsNullOrWhiteSpace(i.Type)).Select(i => new ListItem { Id = i.Type, Text = i.Type }).Distinct()),
                        MapSubTypes = SelectListFrom(dbContext.Maps.Where(i => !string.IsNullOrWhiteSpace(i.SubType)).Select(i => new ListItem { Id = i.Type, Text = i.SubType }).Distinct()),
                        ImageTypes = SelectListFrom(dbContext.Images.Where(i => !string.IsNullOrWhiteSpace(i.Type)).Select(i => new ListItem { Id = i.Type, Text = i.Type }).Distinct()),
                        ImageSubTypes = SelectListFrom(dbContext.Images.Where(i => !string.IsNullOrWhiteSpace(i.SubType)).Select(i => new ListItem { Id = i.Type, Text = i.SubType }).Distinct()),
                        DatasetTypes = SelectListFrom(dbContext.Datasets.SelectMany(i => i.DatasetVariables).Where(i => !string.IsNullOrWhiteSpace(i.Type)).Select(i => new ListItem { Id = i.Type, Text = i.Type }).Distinct()),
                        DatasetSubTypes = SelectListFrom(dbContext.Datasets.SelectMany(i => i.DatasetVariables).Where(i => !string.IsNullOrWhiteSpace(i.SubType)).Select(i => new ListItem { Id = i.Type, Text = i.SubType }).Distinct())
                    };
                    return View(model);
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }


        [HttpGet("Info/{id}/Issues")]
        public List<IssueModel> GetIssues(int id)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id } }))
            {
                try
                {
                    return dbContext.Issues.Where(i => i.EstuaryId == id).Include(i => i.IssueType).OrderBy(i => i.IssueType.Type).ThenBy(i => i.Header).Select(i =>
                        new IssueModel
                        {
                            Id = i.Id,
                            Type = i.IssueType.Type,
                            Header = i.Header,
                            Notes = i.Notes,
                            Responses = i.Responses
                        }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        [HttpPost("Info/{id}/Literature")]
        public List<LiteratureModel> GetLiterature(int id, LiteratureFilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id }, { "Filters", filters } }))
            {
                try
                {
                    var estuary = dbContext.Estuaries.Include(i => i.EstuaryLiteratures).ThenInclude(i => i.Literature).Where(i => i.Id == id).FirstOrDefault();
                    if (estuary == null)
                    {
                        return new List<LiteratureModel>();
                    }

                    var query = estuary.EstuaryLiteratures.Select(i => i.Literature).Where(i => i.Available != "N").AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filters?.Type))
                    {
                        query = query.Where(i => i.Type == filters.Type);
                    }
                    if (!string.IsNullOrWhiteSpace(filters?.SubType))
                    {
                        query = query.Where(i => i.SubType == filters.SubType);
                    }
                    return query
                        .OrderBy(i => i.Type)
                        .ThenBy(i => i.SubType)
                        .ThenBy(i => i.Author)
                        .ThenBy(i => i.Year)
                        .ThenBy(i => i.Title)
                        .Select(i =>
                        new LiteratureModel
                        {
                            Id = i.Id,
                            Type = i.Type,
                            SubType = i.SubType,
                            Author = i.Author,
                            Year = i.Year,
                            Title = string.IsNullOrWhiteSpace(i.Link) || !i.Link.StartsWith("\\SAEDArchive\\") ? i.Title : $"<a target='_blank' href='{i.Link.Replace("SAEDArchive", "Archive")}'>{i.Title}</a>"
                        }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }


        [HttpPost("Info/{id}/Maps")]
        public List<MapModel> GetMaps(int id, MapFilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id }, { "Filters", filters } }))
            {
                try
                {
                    var estuary = dbContext.Estuaries.Include(i => i.EstuaryMaps).ThenInclude(i => i.Map).Where(i => i.Id == id).FirstOrDefault();
                    if (estuary == null)
                    {
                        return new List<MapModel>();
                    }
                    var query = estuary.EstuaryMaps.Select(i => i.Map).Where(i => i.Available != "N").AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filters?.Type))
                    {
                        query = query.Where(i => i.Type == filters.Type);
                    }
                    if (!string.IsNullOrWhiteSpace(filters?.SubType))
                    {
                        query = query.Where(i => i.SubType == filters.SubType);
                    }
                    return query
                        .OrderBy(i => i.Type)
                        .ThenBy(i => i.SubType)
                        .ThenBy(i => i.Name)
                        .Select(i =>
                        new MapModel
                        {
                            Id = i.Id,
                            Type = i.Type,
                            SubType = i.SubType,
                            Name = string.IsNullOrWhiteSpace(i.Link) ? i.Name : $"<a target='_blank' href='{i.Link.Replace("SAEDArchive", "Archive")}'>{i.Name}</a>",
                        }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }


        [HttpPost("Info/{id}/Images")]
        public List<ImageModel> GetImages(int id, ImageFilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id }, { "Filters", filters } }))
            {
                try
                {
                    var estuary = dbContext.Estuaries.Include(i => i.EstuaryImages).ThenInclude(i => i.Image).Where(i => i.Id == id).FirstOrDefault();
                    if (estuary == null)
                    {
                        return new List<ImageModel>();
                    }

                    var query = estuary.EstuaryImages.Select(i => i.Image).Where(i => i.Available != "N").AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filters?.Type))
                    {
                        query = query.Where(i => i.Type == filters.Type);
                    }
                    if (!string.IsNullOrWhiteSpace(filters?.SubType))
                    {
                        query = query.Where(i => i.SubType == filters.SubType);
                    }
                    return query
                        .OrderBy(i => i.Type)
                        .ThenBy(i => i.SubType)
                        .ThenBy(i => i.Date)
                        .ThenBy(i => i.Name)
                        .Select(i =>
                        new ImageModel
                        {
                            Id = i.Id,
                            Type = i.Type,
                            SubType = i.SubType,
                            Name = string.IsNullOrWhiteSpace(i.Link) || !i.Link.StartsWith("\\SAEDArchive\\") ? i.Name : $"<a target='_blank' href='{i.Link.Replace("SAEDArchive", "Archive")}'>{i.Name}</a>",
                            Date = i.Date,
                            Source = i.Source,
                            Reference = i.Reference,
                            Notes = i.Notes
                        }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }


        [HttpPost("Info/{id}/Datasets")]
        public List<DatasetModel> GetDatasets(int id, DatasetFilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new MethodCallParameters { { "Id", id }, { "Filters", filters } }))
            {
                try
                {
                    var estuary = dbContext.Estuaries
                        .Include(i => i.EstuaryDatasets)
                        .ThenInclude(i => i.Dataset)
                        .ThenInclude(i => i.DatasetVariables)
                        .ThenInclude(i => i.Dataset)
                        .Where(i => i.Id == id)
                        .FirstOrDefault();
                    if (estuary == null)
                    {
                        return new List<DatasetModel>();
                    }

                    var query = estuary.EstuaryDatasets.SelectMany(i => i.Dataset.DatasetVariables).AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filters?.Type))
                    {
                        query = query.Where(i => i.Type == filters.Type);
                    }
                    if (!string.IsNullOrWhiteSpace(filters?.SubType))
                    {
                        query = query.Where(i => i.SubType == filters.SubType);
                    }
                    return query
                        .OrderBy(i => i.Type)
                        .ThenBy(i => i.SubType)
                        .ThenBy(i => i.VariableName)
                        .ThenBy(i => i.Dataset.Date)
                        .ThenBy(i => i.Dataset.Name)
                        .Select(i =>
                        new DatasetModel
                        {
                            Id = i.Id,
                            Type = i.Type,
                            SubType = i.SubType,
                            Variable = i.VariableName,
                            Date = i.Dataset.Date,
                            Dataset = string.IsNullOrWhiteSpace(i.Link) ? i.Dataset.Name : $"<a target='_blank' href='{i.Link.Replace("SAEDArchive", "Archive")}'>{i.Dataset.Name}</a>",
                        }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }
    }
}