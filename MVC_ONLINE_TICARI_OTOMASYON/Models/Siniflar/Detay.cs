using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class Detay
    {
        [Key]
        public int DetayID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string urunad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(3000)]
        public string urunbilgi { get; set; }
    }
}