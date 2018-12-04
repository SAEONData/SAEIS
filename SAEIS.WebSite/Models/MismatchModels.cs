using System.Collections.Generic;

namespace SAEIS.WebSite.Models
{
    public class MismatchItemModel
    {
        public string Type { get; set; }
        public string FileName { get; set; }
        public bool InDatabase { get; set; }
        public bool InDirectory { get; set; }
        public bool? Available { get; set; }
    }

    public class MismatchModel
    {
        public List<MismatchItemModel> Items { get; set; } = new List<MismatchItemModel>();
    }
}
