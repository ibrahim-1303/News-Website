using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Son_Hali.Models;

namespace Son_Hali.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ImageDbContext _context;

        public HomeController(ILogger<HomeController> logger,ImageDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Habers.ToListAsync());
        }

        public async Task<IActionResult> Details(int id)
        {
            //if (id==null)
            //{
            //    return NotFound("Bulunamadı");
            //}

            var haber = await _context.Habers.FirstOrDefaultAsync(x => x.HaberId == id);

            if (haber==null)
            {
                return NotFound("Haber Seçilmedi");
            }

            return View(haber);

        }

        public async Task<IActionResult> SonDakika()
        {
            return View(await _context.Habers.ToListAsync());
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
