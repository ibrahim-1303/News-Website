using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Media;
using Microsoft.AspNetCore.Html;

namespace Son_Hali.Models
{
    public class VideoModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Video Name")]
        public string FilePath { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile VideoDosyası { get; set; }
        
       
    }



}