using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace SAEIS.WebSite.Models
{
    public class SearchViewModel
    {
        public string Name { get; set; }

        [DisplayName("Whitfield Classification")]
        public string Classification { get; set; }

        [DisplayName("Whitfield Classification")]
        public SelectList Classifications { get; set; } = null;

        [DisplayName("Biogeographic Region")]
        public string Region { get; set; }

        [DisplayName("Biogeographic Region")]
        public SelectList Regions { get; set; } = null;

        [DisplayName("Estuary Condition")]
        public string Condition { get; set; }

        [DisplayName("Estuary Condition")]
        public SelectList Conditions { get; set; } = null;

        public string Province { get; set; }

        public SelectList Provinces { get; set; } = null;
    }

    public class TableRowModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public string Classification { get; set; }
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