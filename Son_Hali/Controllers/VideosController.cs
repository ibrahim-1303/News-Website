using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Son_Hali.Models;

namespace Son_Hali.Controllers
{
    public class VideosController : Controller
    {
        private readonly ImageDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public VideosController(ImageDbContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
          this._hostEnvironment = hostEnvironment;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            return View(await _context.VideoModels.ToListAsync());
        }

        // GET: Videos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoModel = await _context.VideoModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoModel == null)
            {
                return NotFound();
            }

            return View(videoModel);
        }

        // GET: Videos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FileType,Description,FilePath,VideoYolu")] VideoModel videoModel)
        {
            if (ModelState.IsValid)
            {

                string wwwRoothPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(videoModel.VideoDosyası.FileName);
                string extension = Path.GetExtension(videoModel.VideoDosyası.FileName);
                videoModel.FilePath= fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRoothPath + "/Videolar/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await videoModel.VideoDosyası.CopyToAsync(fileStream);
                }
                _context.Add(videoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        
            return View(videoModel);
        }

        // GET: Videos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoModel = await _context.VideoModels.FindAsync(id);
            if (videoModel == null)
            {
                return NotFound();
            }
            return View(videoModel);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FileType,Description,FilePath,VideoYolu")] VideoModel videoModel)
        {
            if (id != videoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoModelExists(videoModel.Id))
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
            return View(videoModel);
        }

        // GET: Videos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoModel = await _context.VideoModels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (videoModel == null)
            {
                return NotFound();
            }

            return View(videoModel);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoModel = await _context.VideoModels.FindAsync(id);
            _context.VideoModels.Remove(videoModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideoModelExists(int id)
        {
            return _context.VideoModels.Any(e => e.Id == id);
        }
    }
}
