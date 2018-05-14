using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CoreWebApp2.Custom;
using CoreWebApp2.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreWebApp2.Controllers
{
    public class HomeController : Controller
    {
        private SearchViewModel _db = new SearchViewModel();

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(
                model: new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        [HttpGet]
        public async Task<IActionResult> Search(string word)
        {
            try
            {
                var sr = new StreamReader(
                        path: Path.Combine(
                        path1: Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location),
                        path2: @"../../../Models/sites.json"));

                var siteler = JsonConvert.DeserializeObject<List<Site>>(value: sr.ReadToEnd());
                foreach (var item in siteler)
                {
                    var hapServ = new HapServ(
                        url: item.SearchUrlPart1 + word + item.SearcUrlPart2,
                        sitename: item.SiteName,
                        baseUrl: item.BaseUrl,
                        tekrarlanan: item.RepatedItem,
                        isim: item.NameChilds,
                        isimAtr: item.NameAttribute,
                        fiyat: item.PriceChilds,
                        fiyat2: item.PriceChildsTwo,
                        fiyatAtr: item.PriceAttribute,
                        link: item.LinkChilds,
                        linkAtr: item.LinkAttribute,
                        linkExtra: item.LinkExtra,
                        puan: item.SatisfactionChilds,
                        puanAtr: item.SatisfactionAttribute,
                        resim: item.ImageChilds,
                        resimAtr: item.ImageAttribute,
                        satici: item.SellerChilds,
                        saticiAtr: item.SellerAttribute);
                    var data = await hapServ.GetProducts();
                    if (_db.N11Count == 0 && _db.GittigidiyorCount == 0 && _db.HepsiburadaCount == 0)
                    {
                        _db = data;
                    }
                    else
                    {
                        _db.N11Count += data.N11Count;
                        _db.GittigidiyorCount += data.GittigidiyorCount;
                        _db.HepsiburadaCount += data.HepsiburadaCount;
                        _db.Products.AddRange(data.Products);
                        _db.CountPrices.c0_25 += data.CountPrices.c0_25;
                        _db.CountPrices.c25_50 += data.CountPrices.c25_50;
                        _db.CountPrices.c50_100 += data.CountPrices.c50_100;
                        _db.CountPrices.c100_250 += data.CountPrices.c100_250;
                        _db.CountPrices.c250_500 += data.CountPrices.c250_500;
                        _db.CountPrices.c500_1000 += data.CountPrices.c500_1000;
                        _db.CountPrices.c1000_2500 += data.CountPrices.c1000_2500;
                        _db.CountPrices.c2500_5000 += data.CountPrices.c2500_5000;
                        _db.CountPrices.c5000_plus += data.CountPrices.c5000_plus;
                    }
                }

                if (_db == null) return RedirectToAction("Error");
                Console.WriteLine("------------------------------");
                Console.WriteLine(_db.Products.Count);
                Console.WriteLine("------------------------------");
                Console.WriteLine(JsonConvert.SerializeObject(_db.CountPrices));
                Console.WriteLine("------------------------------");
                Console.WriteLine(_db.N11Count);
                Console.WriteLine("------------------------------");
                Console.WriteLine(_db.GittigidiyorCount);
                Console.WriteLine("------------------------------");
                Console.WriteLine(_db.HepsiburadaCount);
                Console.WriteLine("------------------------------");
                //TODO: Tayfuncum Bura sende
                //TODO: JS yi ayarladıktan sonra data=_db yaparsın :D
                return Json(data: new {data = _db, status = true});
            }
            catch (Exception e)
            {
                // TODO: Hata oluştur
                Console.WriteLine(value: e.Message);
                return RedirectToAction(actionName: "Error");
            }
        }
    }
}