#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "601a2639d020f03fffbcef3286ada2313228d13b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Tags_Index), @"mvc.1.0.view", @"/Views/Tags/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\_ViewImports.cshtml"
using NetNews;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\_ViewImports.cshtml"
using NetNews.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"601a2639d020f03fffbcef3286ada2313228d13b", @"/Views/Tags/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Tags_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NetNews.Models.PostsDataModel.vwPostsApprovedModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/HomeLayout/_BreakingNews.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/_ProcessMessage.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("banner"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-dark text-decoration-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("font-weight-bold text-decoration-none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::LazZiya.TagHelpers.PagingTagHelper __LazZiya_TagHelpers_PagingTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
  
    ViewData["Title"] = "Showing results for tag: " + ViewBag.Tag;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<!-- Breaking news partial -->\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "601a2639d020f03fffbcef3286ada2313228d13b6770", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n<div class=\"content-wrapper\">\r\n    <!-- Action messages partial -->\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "601a2639d020f03fffbcef3286ada2313228d13b7969", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
    <div class=""container"">
        <div class=""col-sm-12"">
            <div class=""card"" data-aos=""fade-up"">
                <div class=""card-body"">
                    <div class=""row"">
                        <div class=""col-sm-12"">
                            <h3 class=""font-weight-bold mb-4"">
                                Showing results for tag: <span class=""text-info"">");
#nullable restore
#line 21 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                                                                            Write(TextHelper.ConvertCase(ViewBag.Tag, "SplitUpper"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                            </h3>\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"row\">\r\n                        <div class=\"col-lg-8\">\r\n");
#nullable restore
#line 27 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                             foreach (var item in Model)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"row\">\r\n                                    <div class=\"col-sm-4 grid-margin\">\r\n                                        <div class=\"rotate-img\">\r\n                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "601a2639d020f03fffbcef3286ada2313228d13b10577", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1371, "~/files/", 1371, 8, true);
#nullable restore
#line 32 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
AddHtmlAttributeValue("", 1379, PostHelper.GetPostImageLink(item.PostID), 1379, 41, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                        </div>
                                    </div>
                                    <div class=""col-sm-8 grid-margin"">
                                        <h2 class=""font-weight-600 mb-2"">
                                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "601a2639d020f03fffbcef3286ada2313228d13b12552", async() => {
                WriteLiteral("\r\n                                                ");
#nullable restore
#line 40 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                                           Write(TextHelper.FormatLongText(item.PostTitle, 50));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                                            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "href", 2, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.SingleQuotes);
            AddHtmlAttributeValue("", 1849, "~/Posts/", 1849, 8, true);
#nullable restore
#line 39 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
AddHtmlAttributeValue("", 1857, item.PostPermalink, 1857, 19, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                        </h2>\r\n                                        <p class=\"fs-13 text-muted mb-0\">\r\n                                            <span class=\'mr-2\'>");
#nullable restore
#line 44 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                                                          Write(PageLayoutHelper.GetTimeSince(item.ApprovalsDateAdded));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ago</span>\r\n                                        </p>\r\n                                        <p class=\"fs-15\">\r\n                                            ");
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                                       Write(TextHelper.FormatLongText(TextHelper.StripHTML(item.PostContent), 200));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </p>\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 51 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                             if (!Model.Any())
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <div class=\"alert alert-danger row\">\r\n                                    <div class=\"col-sm-12 col-md-10\">\r\n                                        No posts available for \"");
#nullable restore
#line 57 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                                                           Write(TextHelper.ConvertCase(ViewBag.Category, "SplitUpper"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\"\r\n                                    </div>\r\n                                    <div class=\"col-sm-12 col-md-2\">\r\n                                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "601a2639d020f03fffbcef3286ada2313228d13b16975", async() => {
                WriteLiteral("Home");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_7.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                    </div>\r\n                                </div>\r\n");
#nullable restore
#line 63 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                <!--//BEGIN PAGINATION-->
                                <div class=""row"">
                                    <div class=""col-8 offset-2"">
                                        <div class=""row"">
                                            <div class=""col-12 text-center"">
                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("paging", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "601a2639d020f03fffbcef3286ada2313228d13b19178", async() => {
                WriteLiteral("\r\n                                                ");
            }
            );
            __LazZiya_TagHelpers_PagingTagHelper = CreateTagHelper<global::LazZiya.TagHelpers.PagingTagHelper>();
            __tagHelperExecutionContext.Add(__LazZiya_TagHelpers_PagingTagHelper);
#nullable restore
#line 71 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.TotalRecords = ViewBag.TotalRecords;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("total-records", __LazZiya_TagHelpers_PagingTagHelper.TotalRecords, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 72 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.PageNo = ViewBag.PageNo;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("page-no", __LazZiya_TagHelpers_PagingTagHelper.PageNo, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 73 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.PageSize = ViewBag.PageSize;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("page-size", __LazZiya_TagHelpers_PagingTagHelper.PageSize, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 74 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowPrevNext = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-prev-next", __LazZiya_TagHelpers_PagingTagHelper.ShowPrevNext, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 75 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowFirstLast = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-first-last", __LazZiya_TagHelpers_PagingTagHelper.ShowFirstLast, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 76 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowTotalPages = false;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-total-pages", __LazZiya_TagHelpers_PagingTagHelper.ShowTotalPages, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 77 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowTotalRecords = false;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-total-records", __LazZiya_TagHelpers_PagingTagHelper.ShowTotalRecords, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 78 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowPageSizeNav = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-page-size-nav", __LazZiya_TagHelpers_PagingTagHelper.ShowPageSizeNav, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
#nullable restore
#line 79 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
__LazZiya_TagHelpers_PagingTagHelper.ShowGap = true;

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("show-gap", __LazZiya_TagHelpers_PagingTagHelper.ShowGap, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                                            </div>\r\n                                        </div>\r\n                                    </div>\r\n                                </div>\r\n                                <!--//END PAGINATION-->\r\n");
#nullable restore
#line 86 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                        </div>
                        <div class=""col-lg-4"">
                            <h2 class=""mb-4 text-primary font-weight-600"">
                                Latest news
                            </h2>
                            ");
#nullable restore
#line 93 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                       Write(PageLayoutHelper.GetLatestNewsPosts(3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                            <div class=\"trending\">\r\n                                <h2 class=\"mb-4 text-primary font-weight-600\">\r\n                                    Trending\r\n                                </h2>\r\n                                ");
#nullable restore
#line 99 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Tags\Index.cshtml"
                           Write(PageLayoutHelper.GetTrendingNewsPosts(3));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<NetNews.Models.PostsDataModel.vwPostsApprovedModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
