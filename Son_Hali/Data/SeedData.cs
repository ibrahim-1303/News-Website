using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Son_Hali.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Son_Hali.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Console.WriteLine("Çekirdek Verileri Tohumlanıyor");

            using (var context = new ImageDbContext(serviceProvider.GetRequiredService<DbContextOptions<ImageDbContext>>()))
            {
                context.Database.Migrate();
                if (context.Admins.Any())
                {
                    return;
                }
                context.Admins.AddRange(
                   new Admin
                   {
                       KullanıcıAdı = "İbrahim",
                       Sİfre = "1991"
                   }) ;
                context.Habers.AddRange(
                    new Haber
                    {
                        Baslik = "Haber Başlığı",
                        İcerik = "Haber İçeriği",
                        SonDakika = true,
                        ResimYolu="1.jpg"
                    });


                context.SaveChanges();
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
                logger.LogInformation("Çekirdek Veriler Başarıyla Yazıldı.");
            }


        }


    }
}



