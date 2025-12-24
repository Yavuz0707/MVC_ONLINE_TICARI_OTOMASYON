using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class FaturaKalem
    {
        [Key]
        public int FaturaKalemid { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Aciklama { get; set; }
        public int Miktar { get; set; }

        public decimal BiriFiyat { get; set; }

        public decimal  Tutar {  get; set; }

        public int Faturaid { get; set; }

        public virtual Faturalar Faturalar { get; set; }  // FaturaKalem sınıfı ile Faturalar sınıfı arasında bir ilişki var.


    }
}