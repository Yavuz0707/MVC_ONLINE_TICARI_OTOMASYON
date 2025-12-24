using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class SatisHareket
    {
        [Key]
        public int Satisid { get; set; }

        public DateTime Tarih { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
        public decimal ToplamTutar { get; set; }

        public int Urunid { get; set; }
        public int Cariid { get; set; }
        public int Personelid { get; set; }

        public virtual Urun Urun { get; set; }
        public virtual Cariler Cariler { get; set; }
        public virtual Personel Personel { get; set; }
    }
}
// Bu sınıf, satış hareketlerini temsil eder ve Entity Framework ile veritabanı ile etkileşimde bulunmak için kullanılır.