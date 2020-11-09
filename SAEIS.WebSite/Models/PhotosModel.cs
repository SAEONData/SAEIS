using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAEIS.WebSite.Models
{
    public class PhotosModel
    {
        [BindProperty, Required, DisplayName("Estuary")]
        public int? EstuaryId { get; set; }
        public List<SelectListItem> Estuaries { get; set; } = new List<SelectListItem>();
        public List<ImageModel> Images { get; set; } = new List<ImageModel>();
    }
}
