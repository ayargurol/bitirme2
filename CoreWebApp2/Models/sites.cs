using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace CoreWebApp2.Models
{
    public class sites
    {
        public int siteId { get; set; }
        public string siteName { get; set; }
        public string baseUrl { get; set; }
        public string searchUrlPart1 { get; set; }
        public string searcUrlPart2 { get; set; }
        public string repatedItem { get; set; }
        public List<int> nameChilds { get; set; }
        public string nameAttribute { get; set; }
        public List<int> priceChilds { get; set; }
        public List<int> priceChildsTwo { get; set; }
        public string priceAttribute { get; set; }
        public List<int> linkChilds { get; set; }
        public string linkAttribute { get; set; }
        public string link_extra { get; set; }
        public List<int> imageChilds { get; set; }
        public string imageAttribute { get; set; }
        public List<int> sellerChilds { get; set; }
        public string sellerAttribute { get; set; }
        public List<int> satisfactionChilds { get; set; }
        public string satisfactionAttribute { get; set; }
    }
}
