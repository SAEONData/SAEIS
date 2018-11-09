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
    public class SearchController : Controller
    {
        private SAEISDbContext dbContext = null;

        public SearchController(SAEISDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        ~SearchController()
        {
            dbContext = null;
        }

        public class ListItem
        {
            public int? Id { get; set; } = null;
            public string Text { get; set; }
            public bool IsSelected { get; set; }
        }

        public IActionResult Index()
        {

            SelectList SelectListFrom(IQueryable<ListItem> list)
            {
                var newList = list.OrderBy(i => i.Text).ToList();
                newList.Insert(0, new ListItem { Text = "All", IsSelected = true });
                return new SelectList(newList, "Id", "Text", "All");
            }

            var model = new SearchViewModel
            {
                Classifications = SelectListFrom(dbContext.Classifications.Select(i => new ListItem { Id = i.Id, Text = i.Type })),
                Regions = SelectListFrom(dbContext.Regions.Select(i => new ListItem { Id = i.Id, Text = i.Category })),
                Conditions = SelectListFrom(dbContext.Conditions.Select(i => new ListItem { Id = i.Id, Text = i.Type })),
                Provinces = SelectListFrom(dbContext.Provinces.Select(i => new ListItem { Id = i.Id, Text = i.Name }))
            };
            return View(model);
        }

        private IQueryable<Estuary> GetData(FilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new ParameterList { { "Filters", filters } }))
            {
                try
                {
                    var query = dbContext.Estuaries.AsQueryable();
                    if (!string.IsNullOrWhiteSpace(filters?.Name))
                    {
                        query = query.Where(i => EF.Functions.Like(i.Name,$"%{filters.Name}%"));
                    }
                    if (filters?.Classification.HasValue ?? false)
                    {
                        query = query.Where(i => i.ClassificationId == filters.Classification);
                    }
                    if (filters?.Region.HasValue ?? false)
                    {
                        query = query.Where(i => i.RegionId == filters.Region);
                    }
                    if (filters?.Condition.HasValue ?? false)
                    {
                        query = query.Where(i => i.ConditionId == filters.Condition);
                    }
                    if (filters?.Province.HasValue ?? false)
                    {
                        query = query.Where(i => i.ProvinceId == filters.Province);
                    }
                    return query.OrderBy(i => i.Name);
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        [HttpPost]
        public List<TableRowModel> GetTableData(FilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new ParameterList { { "Filters", filters } }))
            {
                try
                {
                    return GetData(filters).Select(i => new TableRowModel
                    {
                        Id = i.Id,
                        Name = $"<a href='/Info/{i.Id}'>{i.Name}</a>",
                        Classification = i.Classification.Type,
                        Province = i.Province.Name
                    }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        [HttpPost]
        public List<MapPointModel> GetMapData(FilterModel filters)
        {
            using (Logging.MethodCall(GetType(), new ParameterList { { "Filters", filters } }))
            {
                try
                {
                    return GetData(filters).Where(i => i.Latitude.HasValue && i.Longitude.HasValue).Select(i => new MapPointModel
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Url = $"/Info/{i.Id}",
                        Link = $"<a href='/Info/{i.Id}'>{i.Name}</a>",
                        Latitude = -i.Latitude.Value,
                        Longitude = i.Longitude.Value
                    }).ToList();
                }
                catch (Exception ex)
                {
                    Logging.Exception(ex);
                    throw;
                }
            }
        }

        public IActionResult Info(int id)
        {
            return View();
        }
    }
}