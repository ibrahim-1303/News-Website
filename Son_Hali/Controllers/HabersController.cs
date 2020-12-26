using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Son_Hali.Models;

namespace Son_Hali.Controllers
{
    public class HabersController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HabersController(ImageDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

      

        [Authorize]
        // GET: Habers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Habers.ToListAsync());
        }

        // GET: Habers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haber = await _context.Habers
                .FirstOrDefaultAsync(m => m.HaberId == id);
            if (haber == null)
            {
                return NotFound();
            }

            return View(haber);
        }

        // GET: Habers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Habers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HaberId,Baslik,İcerik,SonDakika,ResimDosyası")] Haber haber)
        {
            if (ModelState.IsValid)
            {

                string wwwRoothPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(haber.ResimDosyası.FileName);
                string extension = Path.GetExtension(haber.ResimDosyası.FileName);
                haber.ResimYolu = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRoothPath + "/HaberResimleri/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await haber.ResimDosyası.CopyToAsync(fileStream);
                }
                _context.Add(haber);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haber);
        }

        // GET: Habers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haber = await _context.Habers.FindAsync(id);
            if (haber == null)
            {
                return NotFound();
            }
            return View(haber);
        }

        // POST: Habers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HaberId,Baslik,İcerik,SonDakika,ResimYolu")] Haber haber)
        {
            if (id != haber.HaberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(haber);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaberExists(haber.HaberId))
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
            return View(haber);
        }

        // GET: Habers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var haber = await _context.Habers
                .FirstOrDefaultAsync(m => m.HaberId == id);
            if (haber == null)
            {
                return NotFound();
            }

            return View(haber);
        }

        // POST: Habers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var haber = await _context.Habers.FindAsync(id);

            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "HaberResimleri", haber.ResimYolu);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            _context.Habers.Remove(haber);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HaberExists(int id)
        {
            return _context.Habers.Any(e => e.HaberId == id);
        }
    }
}
