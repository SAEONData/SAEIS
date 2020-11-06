using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace SAEIS.WebSite.Models
{
    public class PhotosModel
    {
        [BindProperty]
        public string Estuary { get; set; }
        public List<SelectListItem> Estuaries { get; set; } = new List<SelectListItem>();
    }
}
