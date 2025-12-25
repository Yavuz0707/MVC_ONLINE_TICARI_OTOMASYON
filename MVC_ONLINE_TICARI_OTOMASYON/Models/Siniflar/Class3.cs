using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_ONLINE_TICARI_OTOMASYON.Models.Siniflar
{
    public class Class3
    {

        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> Urunler { get; set; }

    }
}