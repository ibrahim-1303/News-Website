using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Son_Hali.Models;

namespace Son_Hali.Models
{
    public class ImageDbContext:DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options) :base(options)
        {
            
        }

        public DbSet<ImageModel> Images{ get; set; }
        public DbSet<Haber>Habers { get; set; }
        public DbSet<Admin>Admins{ get; set; }
 
        public DbSet<VideoModel> VideoModels { get; set; }

    }
}
