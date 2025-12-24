using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class Kategori
    {
        [Key] //Üzerinde bulunduğu sütunu birincil anahtar yapar.
        public int KategoriID { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string KategoriAd { get; set; }

        public ICollection<Urun> uruns { get; set; }

        //Herbir kategoride birden fazla urun olabilir ama her bir ürünün sadece bir kategorisi olmalı.

    }
}