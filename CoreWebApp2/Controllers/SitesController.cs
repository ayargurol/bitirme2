using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CoreWebApp2.Models.Sql;

namespace CoreWebApp2.Controllers
{
    public class SitesController : Controller
    {
        private readonly SitesContext _context;

        public SitesController(SitesContext context)
        {
            _context = context;
        }

        // GET: Sites
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sites.ToListAsync());
        }

        // GET: Sites/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesDB = await _context.Sites
                .SingleOrDefaultAsync(m => m.SiteId == id);
            if (sitesDB == null)
            {
                return NotFound();
            }

            return View(sitesDB);
        }

        // GET: Sites/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,SiteName,BaseUrl,SearchUrlPart1,SearcUrlPart2,RepatedItem,NameChilds,NameAttribute,PriceChilds,PriceChildsTwo,PriceAttribute,LinkChilds,LinkAttribute,LinkExtra,ImageChilds,ImageAttribute,SellerChilds,SellerAttribute,SatisfactionChilds,SatisfactionAttribute")] SitesDB sitesDB)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sitesDB);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sitesDB);
        }

        // GET: Sites/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesDB = await _context.Sites.SingleOrDefaultAsync(m => m.SiteId == id);
            if (sitesDB == null)
            {
                return NotFound();
            }
            return View(sitesDB);
        }

        // POST: Sites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiteId,SiteName,BaseUrl,SearchUrlPart1,SearcUrlPart2,RepatedItem,NameChilds,NameAttribute,PriceChilds,PriceChildsTwo,PriceAttribute,LinkChilds,LinkAttribute,LinkExtra,ImageChilds,ImageAttribute,SellerChilds,SellerAttribute,SatisfactionChilds,SatisfactionAttribute")] SitesDB sitesDB)
        {
            if (id != sitesDB.SiteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sitesDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SitesDBExists(sitesDB.SiteId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sitesDB);
        }

        // GET: Sites/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sitesDB = await _context.Sites
                .SingleOrDefaultAsync(m => m.SiteId == id);
            if (sitesDB == null)
            {
                return NotFound();
            }

            return View(sitesDB);
        }

        // POST: Sites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sitesDB = await _context.Sites.SingleOrDefaultAsync(m => m.SiteId == id);
            _context.Sites.Remove(sitesDB);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SitesDBExists(int id)
        {
            return _context.Sites.Any(e => e.SiteId == id);
        }
    }
}
