using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Son_Hali.Models
{
    public class Haber
    {
        [Key]
        public int HaberId { get; set; }
        public string Baslik { get; set; }

        public string İcerik { get; set; }

        public bool SonDakika { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Image Name")]
        public string ResimYolu { get; set; }


        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ResimDosyası{ get; set; }
    }
}
