using Microsoft.AspNetCore.Mvc;
using SAEIS.Data;
using SimpleMvcSitemap;
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
            foreach (var estuary in dbContext.Estuaries.OrderBy(i => i.Name))
            {
                nodes.Add(new SitemapNode(Url.Action("Index","Info",new { id = estuary.Id })));
            }
            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}