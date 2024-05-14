using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Moqups.Infrastructure.TagHelpers
{
    [HtmlTargetElement("table")] //HTML tanimi olmali - tablo ile calisilacak
    public class TableTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.SetAttribute("class", "table table-hover table-bordered"); 
            //Olusturlan tablolar otomatik Bootstrap'in table ozelligini alir.
        }
    }
}
