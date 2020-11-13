using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAEIS.WebSite.Models
{
    public class ImagesModel
    {
        public int EstuaryId { get; set; }
        public string EstuaryName { get; set; }
        public List<ImageModel> Images { get; set; }
    }

    public class ImageModel
    {
        public int RowNum { get; set; }
        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public List<SelectListItem> Types { get; set; }
        public string SubType { get; set; }
        public List<SelectListItem> SubTypes { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Date { get; set; }
        public string LinkToImage { get; set; }
        public string LinkToHiResImage { get; set; }
        public long? HiResImageSize { get; set; }
        [Required]
        public string Source { get; set; }
        public List<SelectListItem> Sources { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }

    }
}
