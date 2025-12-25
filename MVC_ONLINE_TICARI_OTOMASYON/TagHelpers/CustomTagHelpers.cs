using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace MVC_ONLINE_TICARI_OTOMASYON.TagHelpers
{
    /// <summary>
    /// Email adresi için özel tag helper
    /// Kullanım: <email address="ornek@email.com"></email>
    /// </summary>
    [HtmlTargetElement("email")]
    public class EmailTagHelper : TagHelper
    {
        public string Address { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{Address}");
            output.Attributes.SetAttribute("class", "text-primary");
            output.Content.SetContent(Address);
        }
    }

    /// <summary>
    /// Telefon numarası için özel tag helper
    /// Kullanım: <phone number="0555 123 45 67"></phone>
    /// </summary>
    [HtmlTargetElement("phone")]
    public class PhoneTagHelper : TagHelper
    {
        public string Number { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"tel:{Number.Replace(" ", "")}");
            output.Attributes.SetAttribute("class", "text-success");
            output.Content.SetHtmlContent($"<i class='fas fa-phone'></i> {Number}");
        }
    }

    /// <summary>
    /// Bootstrap card oluşturan tag helper
    /// Kullanım: <card-box title="Başlık" color="primary">İçerik</card-box>
    /// </summary>
    [HtmlTargetElement("card-box")]
    public class CardBoxTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Color { get; set; } = "primary";
        public string Icon { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"card border-{Color}");

            var headerIcon = string.IsNullOrEmpty(Icon) ? "" : $"<i class='{Icon}'></i> ";
            
            var content = new StringBuilder();
            content.Append($"<div class='card-header bg-{Color} text-white'>");
            content.Append($"<h5 class='card-title mb-0'>{headerIcon}{Title}</h5>");
            content.Append("</div>");
            content.Append("<div class='card-body'>");
            content.Append(output.GetChildContentAsync().Result.GetContent());
            content.Append("</div>");

            output.Content.SetHtmlContent(content.ToString());
        }
    }

    /// <summary>
    /// Alert box oluşturan tag helper
    /// Kullanım: <alert-box type="success">Mesaj</alert-box>
    /// </summary>
    [HtmlTargetElement("alert-box")]
    public class AlertBoxTagHelper : TagHelper
    {
        public string Type { get; set; } = "info";
        public bool Dismissible { get; set; } = true;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            
            var classes = $"alert alert-{Type}";
            if (Dismissible)
                classes += " alert-dismissible fade show";

            output.Attributes.SetAttribute("class", classes);
            output.Attributes.SetAttribute("role", "alert");

            var content = new StringBuilder();
            content.Append(output.GetChildContentAsync().Result.GetContent());
            
            if (Dismissible)
            {
                content.Append(@"
                    <button type='button' class='close' data-dismiss='alert' aria-label='Close'>
                        <span aria-hidden='true'>&times;</span>
                    </button>");
            }

            output.Content.SetHtmlContent(content.ToString());
        }
    }

    /// <summary>
    /// İstatistik kutusu oluşturan tag helper
    /// Kullanım: <stat-box title="Toplam Satış" value="1250" icon="fas fa-shopping-cart" color="success"></stat-box>
    /// </summary>
    [HtmlTargetElement("stat-box")]
    public class StatBoxTagHelper : TagHelper
    {
        public string Title { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public string Color { get; set; } = "primary";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "small-box bg-" + Color);

            var content = $@"
                <div class='inner'>
                    <h3>{Value}</h3>
                    <p>{Title}</p>
                </div>
                <div class='icon'>
                    <i class='{Icon}'></i>
                </div>";

            output.Content.SetHtmlContent(content);
        }
    }
}
