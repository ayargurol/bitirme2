using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CoreWebApp2.Custom;
using CoreWebApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreWebApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly List<product> _db = new List<product>();

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PriceFiltered(List<product> sort)
        {
            var sorted = Filters.PriceSort(sort);
            return View(sorted);
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
                var siteler = JsonConvert.DeserializeObject<List<Sites>>(sr.ReadToEnd());
                foreach (var item in siteler)
                {
                    var hapServ = new HapServ(
                        item.SearchUrlPart1 + word + item.SearcUrlPart2,
                        item.BaseUrl,
                        item.RepatedItem,
                        item.NameChilds,
                        item.NameAttribute,
                        item.PriceChilds,
                        item.PriceChildsTwo,
                        item.PriceAttribute,
                        item.LinkChilds,
                        item.LinkAttribute,
                        item.LinkExtra,
                        item.SatisfactionChilds,
                        item.SatisfactionAttribute,
                        item.ImageChilds,
                        item.ImageAttribute,
                        item.SellerChilds,
                        item.SellerAttribute);
                    _db.AddRange(await hapServ.GetProducts());
                }
                // Bug: filters.PriceSort => Line 38,39 StartIndex  null Error
                //var sorted = filters.PriceSort(this.db);
                var sorted = _db;
                if (_db == null) return RedirectToAction("Error");

                Console.WriteLine("------------------------------");
                Console.WriteLine(sorted.Count);
                Console.WriteLine("------------------------------");

                return Json(new { data = _db, status = true });
            }
            catch (Exception e)
            {
                // TODO: Hata oluştur
                Console.WriteLine(e.Message);
                return RedirectToAction("Error");
            }
        }
    }
}