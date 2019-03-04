using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAEIS.Data;
using SimpleMvcSitemap;
using SimpleMvcSitemap.Images;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAEIS.WebSite.Controllers
{
    public class SitemapController : Controller
    {
        private SAEISDbContext dbContext = null;

        public SitemapController(SAEISDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        ~SitemapController()
        {
            dbContext = null;
        }

        [Route("sitemap.xml")]
        [ResponseCache(Duration = 604800)]
        public IActionResult Index()
        {
            List<SitemapNode> nodes = new List<SitemapNode>
            {
                new SitemapNode(Url.Action("Index","Home")),
                new SitemapNode(Url.Action("Index","Search")),
                new SitemapNode(Url.Action("About","Home")),
                new SitemapNode(Url.Action("Acknowledgements","Home")),
                new SitemapNode(Url.Action("Background","Home")),
                new SitemapNode(Url.Action("Contact","Home")),
                new SitemapNode(Url.Action("Links","Home")),
                new SitemapNode(Url.Action("Privacy","Home")),
                new SitemapNode(Url.Action("Researchers","Home")),
            };
            foreach (var estuary in dbContext.Estuaries.Include(i => i.EstuaryLiteratures).ThenInclude(i => i.Literature).Include(i => i.EstuaryImages).ThenInclude(i => i.Image).OrderBy(i => i.Name))
            {
                var node = new SitemapNode(Url.Action("Index", "Info", new { id = estuary.Id }));
                var images = new List<SitemapImage>();
                foreach (var image in estuary.EstuaryImages.Select(i => i.Image).Where(i => !string.IsNullOrWhiteSpace(i.Link) && i.Link.StartsWith("\\SAEDArchive\\") && i.Available != "No").OrderBy(i => i.Name))
                {
                    var uri = $"{Request.Scheme}://{Request.Host}{image.Link.Replace("SAEDArchive", "Archive").Replace("\\", "/")}";
                    var sitemapImage = new SitemapImage(Uri.EscapeUriString(uri))
                    {
                        Title = image.Name,
                        License = "https://creativecommons.org/licenses/by-nc-sa/4.0/"
                    };
                    images.Add(sitemapImage);
                    //nodes.Add(new SitemapNode(Uri.EscapeUriString(uri)));
                }
                node.Images = images;
                nodes.Add(node);
                //foreach (var image in estuary.EstuaryImages.Select(i => i.Image).Where(i => !string.IsNullOrWhiteSpace(i.Link) && i.Link.StartsWith("\\SAEDArchive\\") && i.Available != "No").OrderBy(i => i.Name))
                //{
                //    var uri = $"{Request.Scheme}://{Request.Host}{image.Link.Replace("SAEDArchive", "Archive").Replace("\\", "/")}";
                //    nodes.Add(new SitemapNode(Uri.EscapeUriString(uri)));
                //}
                foreach (var literature in estuary.EstuaryLiteratures.Select(i => i.Literature).Where(i => !string.IsNullOrWhiteSpace(i.Link) && i.Link.StartsWith("\\SAEDArchive\\") && i.Available != "No").OrderBy(i => i.Title))
                {
                    var uri = $"{Request.Scheme}://{Request.Host}{literature.Link.Replace("SAEDArchive", "Archive").Replace("\\", "/")}";
                    nodes.Add(new SitemapNode(Uri.EscapeUriString(uri)));
                }
            }
            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}