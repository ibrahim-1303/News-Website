using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Son_Hali.Models;

namespace Son_Hali.Controllers
{
    public class AdminController : Controller
    {
        private readonly ImageDbContext _context;

        public AdminController(ImageDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GirisYap()
        {
            return View();
        }

        public async Task<IActionResult> GirisYap(Admin admin)
        {
            var giris = _context.Admins.FirstOrDefault(x =>
                x.KullanıcıAdı == admin.KullanıcıAdı && x.Sİfre == admin.Sİfre);

            if (giris != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, value: giris.KullanıcıAdı)
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Habers");
            }

            if (giris == null)
            {
                return NotFound("Bilgilerr eşleşmedi");
            }

            return View();
        }

        public IActionResult CikisYap()
        {

            HttpContext.SignOutAsync();
            return RedirectToAction("GirisYap");
        }
    }
}