using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        private readonly List<product> _db = new List<product>();

        public IActionResult Error()
        {
            return View(
                new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Search(string word)
        {
            try
            {
                var sr = new StreamReader(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"../../../Models/sites.json"));
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
                var sorted = _db;
                if (_db == null) return RedirectToAction("Error");
                Console.WriteLine("------------------------------");
                Console.WriteLine(sorted.Count);
                Console.WriteLine("------------------------------");
                return Json(new {data = _db, status = true});
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