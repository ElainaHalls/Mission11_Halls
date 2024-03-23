using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using onlineBookstore.Models.ViewModels;

namespace onlineBookstore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        // This allows us to help build the URL
        private IUrlHelperFactory urlHelperFactory;

        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            urlHelperFactory = temp;
        }

        // Allows us to know what view we are on
        [ViewContext]
        [HtmlAttributeNotBound] 
        public ViewContext? ViewContext { get; set; }

        // Keep track of which action we are on in our controller
        public string? PageAction { get; set; }

        public PaginationInfo PageModel { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (ViewContext != null && PageModel != null)
            {
                IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);

                TagBuilder result = new TagBuilder("div");

                for (int i =1; i <= PageModel.TotalPages; i++)
                {
                    TagBuilder tag = new TagBuilder("a");

                    tag.Attributes["href"] = urlHelper.Action(PageAction, new { pageNum = i });
                    tag.InnerHtml.Append(i.ToString());

                    result.InnerHtml.AppendHtml(tag);

                }

                output.Content.AppendHtml(result.InnerHtml);
            }
        }
    }
}
