using Microsoft.AspNetCore.Mvc.Rendering;
using SAEIS.WebSite.Data;
using System.Collections.Generic;
using System.ComponentModel;

namespace SAEIS.WebSite.Models
{
    public class InfoViewModel
    {
        public Estuary Estuary { get; set; }
        [DisplayName("Type")]
        public string LiteratureType { get; set; }
        public List<SelectListItem> LiteratureTypes { get; set; } = null;
        [DisplayName("Sub type")]
        public string LiteratureSubType { get; set; }
        public List<SelectListItem> LiteratureSubTypes { get; set; } = null;
        [DisplayName("Type")]
        public string MapType { get; set; }
        public List<SelectListItem> MapTypes { get; set; } = null;
        [DisplayName("Sub type")]
        public string MapSubType { get; set; }
        public List<SelectListItem> MapSubTypes { get; set; } = null;
        [DisplayName("Type")]
        public string ImageType { get; set; }
        public List<SelectListItem> ImageTypes { get; set; } = null;
        [DisplayName("Sub type")]
        public string ImageSubType { get; set; }
        public List<SelectListItem> ImageSubTypes { get; set; } = null;
        [DisplayName("Type")]
        public string DatasetType { get; set; }
        public List<SelectListItem> DatasetTypes { get; set; } = null;
        [DisplayName("Sub type")]
        public string DatasetSubType { get; set; }
        public List<SelectListItem> DatasetSubTypes { get; set; } = null;
    }

    public class IssueModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Header { get; set; }
        public string Notes { get; set; }
        public string Responses { get; set; }
    }

    public class LiteratureModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string Title { get; set; }
    }

    public class LiteratureFilterModel
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

    public class MapModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Name { get; set; }
    }

    public class MapFilterModel
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

    //public class ImageModel
    //{
    //    public int RowNum { get; set; }
    //    public int Id { get; set; }
    //    public string Type { get; set; }
    //    public string SubType { get; set; }
    //    public string Name { get; set; }
    //    public string LinkToImage { get; set; }
    //    public string LinkToHiResImage { get; set; }
    //    public long? HiResImageSize { get; set; }
    //    public string Date { get; set; }
    //    public string Source { get; set; }
    //    public string Reference { get; set; }
    //    public string Notes { get; set; }
    //}

    public class ImageFilterModel
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

    public class DatasetModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Variable { get; set; }
        public string Date { get; set; }
        public string Dataset { get; set; }
    }

    public class DatasetFilterModel
    {
        public string Type { get; set; }
        public string SubType { get; set; }
    }

}
