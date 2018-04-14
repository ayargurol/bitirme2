namespace CoreWebApp2.Controllers
{
    using CoreWebApp2.Custom;
    using CoreWebApp2.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    public class HomeController : Controller
    {
        private readonly List<product> db = new List<product>();

        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult PriceFiltered(List<product> sort)
        {
            var sorted = filters.PriceSort(sort);
            return this.View(sorted);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string word)
        {
            try
            {
                #region old

                //await Allsites(word);
                // PARAMETERS: HtmlAgilityPackService( url , tekrar eden tag , ürün ismi xpath , fiyat xpath , link xpath , link attribute , resim xpath , resim attribute (VARSA) satici xpatch , satıcı attribute , puan xpath , puan attribute )
                /*
                HtmlAgilityPackService hapitem = new HtmlAgilityPackService(
                 @"https://www.n11.com/arama?q=" + word + "&srt=SALES_VOLUME",    //url
                 @"//li[@class='column ']",                                       //tekrar_xpath
                 @"//h3",                                                         //isim_xpath
                 null,
                 @"//a[@class='newPrice']/ins",                                   //fiyat_xpath
                 null,
                 @"/div[@class='columnContent ']/div[@class='pro']/a",            //link_xpath
                 @"href",                                                         //link_atr
                 @"/div/div/a/img",                                               //resim_xpath
                 @"data-original",                                                //resim_atr
                 @"/div/a[@class='sallerInfo']",                                  //satici_xpath
                 @"title",                                                        //satici_atr
                 @"/div/a/div/div[@class='pointBar']/span[@class='point']",       //puan_xpath
                 null                                                             //puan_atr
                 );
                 */

                #endregion old

                var sr = new StreamReader(
                    "C:/Users/Gurol/Belgelerim/Visual Studio 2017 Projects/CoreWebApp2/CoreWebApp2/Models/sites.json");
                var siteler = JsonConvert.DeserializeObject<List<sites>>(sr.ReadToEnd());
                foreach (var item in siteler)
                {
                    var hapServ = new hapServ(
                        item.searchUrlPart1 + word + item.searcUrlPart2,
                        item.baseUrl,
                        item.repatedItem,
                        item.nameChilds,
                        item.nameAttribute,
                        item.priceChilds,
                        item.priceChildsTwo,
                        item.priceAttribute,
                        item.linkChilds,
                        item.linkAttribute,
                        item.link_extra,
                        item.satisfactionChilds,
                        item.satisfactionAttribute,
                        item.imageChilds,
                        item.imageAttribute,
                        item.sellerChilds,
                        item.sellerAttribute);
                    this.db.AddRange(await hapServ.GetProducts());
                }
                // Bug: filters.PriceSort => Line 38,39 StartIndex  null Error
                //var sorted = filters.PriceSort(this.db);
                var sorted = this.db;
                if (this.db == null) return this.RedirectToAction("Error");

                Console.WriteLine("------------------------------");
                Console.WriteLine(sorted.Count);
                Console.WriteLine("------------------------------");

                return Json(new { data = db, status = true });
            }
            catch (Exception e)
            {
                // TODO: Hata oluştur
                Console.WriteLine(e.Message);
                return this.RedirectToAction("Error");
            }
        }
    }
}