using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWebApp2.Models
{
    public class Site
    {
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public string BaseUrl { get; set; }
        public string SearchUrlPart1 { get; set; }
        public string SearcUrlPart2 { get; set; }
        public string RepatedItem { get; set; }
        public List<int> NameChilds { get; set; }
        public string NameAttribute { get; set; }
        public List<int> PriceChilds { get; set; }
        public List<int> PriceChildsTwo { get; set; }
        public string PriceAttribute { get; set; }
        public List<int> LinkChilds { get; set; }
        public string LinkAttribute { get; set; }
        public string LinkExtra { get; set; }
        public List<int> ImageChilds { get; set; }
        public string ImageAttribute { get; set; }
        public List<int> SellerChilds { get; set; }
        public string SellerAttribute { get; set; }
        public List<int> SatisfactionChilds { get; set; }
        public string SatisfactionAttribute { get; set; }
    }
}
