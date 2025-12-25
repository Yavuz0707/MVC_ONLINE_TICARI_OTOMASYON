using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;

namespace MVC_ONLINE_TICARI_OTOMASYON.Helpers
{
    /// <summary>
    /// Custom Html Helper metodları
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Bootstrap Alert Box oluşturur
        /// </summary>
        public static IHtmlContent AlertBox(this IHtmlHelper helper, string message, string alertType = "info")
        {
            var alertClass = alertType switch
            {
                "success" => "alert-success",
                "danger" => "alert-danger",
                "warning" => "alert-warning",
                _ => "alert-info"
            };

            var html = $@"
                <div class='alert {alertClass} alert-dismissible fade show' role='alert'>
                    <strong>{alertType.ToUpper()}!</strong> {message}
                    <button type='button' class='close' data-dismiss='alert' aria-label='Close'>
                        <span aria-hidden='true'>&times;</span>
                    </button>
                </div>";

            return new HtmlString(html);
        }

        /// <summary>
        /// Bootstrap Badge oluşturur
        /// </summary>
        public static IHtmlContent Badge(this IHtmlHelper helper, string text, string badgeType = "primary")
        {
            var html = $"<span class='badge badge-{badgeType}'>{text}</span>";
            return new HtmlString(html);
        }

        /// <summary>
        /// İkon ile birlikte buton oluşturur
        /// </summary>
        public static IHtmlContent IconButton(this IHtmlHelper helper, string text, string icon, string cssClass = "btn-primary", string href = "#")
        {
            var html = $@"
                <a href='{href}' class='btn {cssClass}'>
                    <i class='{icon}'></i> {text}
                </a>";
            
            return new HtmlString(html);
        }

        /// <summary>
        /// Durum etiketi oluşturur (Aktif/Pasif)
        /// </summary>
        public static IHtmlContent StatusLabel(this IHtmlHelper helper, bool isActive)
        {
            var status = isActive ? "Aktif" : "Pasif";
            var badgeClass = isActive ? "badge-success" : "badge-danger";
            var html = $"<span class='badge {badgeClass}'>{status}</span>";
            
            return new HtmlString(html);
        }

        /// <summary>
        /// Tarih formatını özel formatta gösterir
        /// </summary>
        public static IHtmlContent FormattedDate(this IHtmlHelper helper, DateTime? date, string format = "dd.MM.yyyy")
        {
            if (date == null)
                return new HtmlString("<span class='text-muted'>Tarih Yok</span>");

            var formattedDate = date.Value.ToString(format);
            return new HtmlString($"<span class='text-primary'><i class='far fa-calendar-alt'></i> {formattedDate}</span>");
        }

        /// <summary>
        /// Para birimi formatında gösterir
        /// </summary>
        public static IHtmlContent Currency(this IHtmlHelper helper, decimal? amount, string currencySymbol = "₺")
        {
            if (amount == null)
                return new HtmlString("<span class='text-muted'>0 ₺</span>");

            var formatted = amount.Value.ToString("N2");
            return new HtmlString($"<span class='text-success'><strong>{formatted} {currencySymbol}</strong></span>");
        }
    }
}
