using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class Departman
    {
        [Key]
        public int Departmanid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string DepartmanAd { get; set; }

        public bool Durum {  get; set; } //Silme işlemi için Yeni Durum sutunu eklendi..

        public ICollection<Personel> Personels { get; set; }
    }
}