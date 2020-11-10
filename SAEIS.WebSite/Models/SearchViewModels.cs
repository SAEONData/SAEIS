using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace SAEIS.WebSite.Models
{
    public class SearchViewModel
    {
        public string Name { get; set; }

        [DisplayName("Whitfield Classification")]
        public string Classification { get; set; }

        [DisplayName("Whitfield Classification")]
        public List<SelectListItem> Classifications { get; set; } = null;

        [DisplayName("Biogeographic Region")]
        public string Region { get; set; }

        [DisplayName("Biogeographic Region")]
        public List<SelectListItem> Regions { get; set; } = null;

        [DisplayName("Estuary Condition")]
        public string Condition { get; set; }

        [DisplayName("Estuary Condition")]
        public List<SelectListItem> Conditions { get; set; } = null;

        public string Province { get; set; }

        public List<SelectListItem> Provinces { get; set; } = null;
    }

    public class EstuaryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Classification { get; set; }
        public string Region { get; set; }
        public string Condition { get; set; }
        public string Province { get; set; }
    }

    public class MapPointModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Url { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsSelected { get; set; }
    }

    public class FilterModel
    {
        public string Name { get; set; }
        public int? Classification { get; set; }
        public int? Region { get; set; }
        public int? Condition { get; set; }
        public int? Province { get; set; }

    }

}