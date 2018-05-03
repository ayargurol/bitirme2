using System.Collections.Generic;

namespace CoreWebApp2.Models
{
    public class SearchViewModel
    {
        public int N11Count { get; set; }
        public int GittigidiyorCount { get; set; }
        public int HepsiburadaCount { get; set; }
        public Prices CountPrices { get; set; }
        public List<product> Products { get; set; }

        public SearchViewModel()
        {
            N11Count = GittigidiyorCount = HepsiburadaCount = 0;
            CountPrices=new Prices();
            Products = new List<product>();
        }
    }
}