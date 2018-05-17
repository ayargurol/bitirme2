using CoreWebApp2.Custom;
using CoreWebApp2.Models;
using CoreWebApp2.Models.Sql;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApp2.Controllers
{
    public class HomeController : Controller
    {
        private readonly SitesContext _context;

        public HomeController(SitesContext context) => _context = context;

        public IActionResult Index() => View();

        public IActionResult Error() => View(model: new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        [HttpGet]
        public async Task<IActionResult> Search(string word)
        {
            SitesConverter sc = new SitesConverter();
            var newList = new List<SearchModel>();
            try
            {
                var siteler = sc.GetSites(_context, word);
                foreach (var item in siteler)
                {
                    newList.Add(await item.GetProducts());
                }

                if (newList == null || newList.Count == 0) return RedirectToAction("Error");

                var kontrol = JsonConvert.SerializeObject(newList);

                Record(word);
                return Json(data: new { data = newList, status = true });
            }
            catch (Exception e)
            {
                Console.WriteLine(value: e.Message);
                return RedirectToAction("Error");
            }
        }

        public async void Record(string word)
        {
            try
            {
                var rec = _context.Records.FirstOrDefault(r => r.SearchedWord == word.ToLower());
                if (rec != null)
                {
                    rec.count++;
                }
                else
                {
                    await _context.Records.AddAsync(new SearchedItem()
                    {
                        SearchedWord = word,
                        count = 1
                    });
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("\nARAMA KAYDEDILEMEDI --->");
                Console.WriteLine(e.Message + "\n");
            }
        }
    }
}