#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "479dcf27e068c4fc03ce7028a3627a3fb158a1ee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout_PostDetails__VideoPostView), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/PostDetails/_VideoPostView.cshtml")]
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
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\_ViewImports.cshtml"
using NetNews;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\_ViewImports.cshtml"
using NetNews.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"479dcf27e068c4fc03ce7028a3627a3fb158a1ee", @"/Views/Shared/AccountLayout/PostDetails/_VideoPostView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout_PostDetails__VideoPostView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.PostsDataModel.vwPostsModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RedirectAuthor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Authors", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration-none text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n    <div class=\"col-10 offset-1\">\r\n\r\n        <!-- Title -->\r\n        <h1 class=\"mt-4\">\r\n            ");
#nullable restore
#line 8 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
       Write(Html.DisplayFor(model => model.PostTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </h1>\r\n\r\n        <!-- Author -->\r\n        <p class=\"lead\">\r\n            by\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "479dcf27e068c4fc03ce7028a3627a3fb158a1ee5458", async() => {
                WriteLiteral("\r\n                ");
#nullable restore
#line 15 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
           Write(AccountHelper.GetAccountData(Model.PostAuthor, "FullName"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 14 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
                                                                                      WriteLiteral(Model.PostAuthor);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </p>\r\n\r\n        <hr>\r\n\r\n        <!-- Preview Image -->\r\n        <div class=\"row\">\r\n");
#nullable restore
#line 23 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
             if (Model.PostVideoType.ToLower() == "youtube")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-12 col-md-12\">\r\n                    <div class=\"embed-responsive embed-responsive-16by9\">\r\n                        <iframe class=\"embed-responsive-item\"");
            BeginWriteAttribute("src", " src=\"", 878, "\"", 964, 2);
            WriteAttributeValue("", 884, "https://www.youtube.com/embed/", 884, 30, true);
#nullable restore
#line 27 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
WriteAttributeValue("", 914, PostHelper.GetYouTubeVideoID(Model.PostVideoLink), 914, 50, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" allowfullscreen></iframe>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 30 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
            }
            else if (Model.PostVideoType.ToLower() == "vimeo")
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-12 col-md-12\">\r\n                    <div class=\"embed-responsive embed-responsive-16by9\">\r\n                        <iframe class=\"embed-responsive-item\"");
            BeginWriteAttribute("src", " src=\"", 1326, "\"", 1411, 2);
            WriteAttributeValue("", 1332, "https://player.vimeo.com/video/", 1332, 31, true);
#nullable restore
#line 35 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
WriteAttributeValue("", 1363, PostHelper.GetVimeoVideoID(Model.PostVideoLink), 1363, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" allowfullscreen></iframe>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 38 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n\r\n        <hr>\r\n        <!-- Date/Time -->\r\n        <p>\r\n            ");
#nullable restore
#line 44 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
       Write(PostHelper.FormatPostDate(Model.DateAdded.ToString()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <span class=\"float-right\">\r\n                <small>Editor: ");
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
                          Write(AccountHelper.GetAccountData(Model.PostEditor, "FullName"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n            </span>\r\n        </p>\r\n\r\n        <hr>\r\n\r\n");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
         if (!string.IsNullOrEmpty(Model.PostContent))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                ");
#nullable restore
#line 56 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
           Write(Html.Raw(Model.PostContent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <hr>\r\n");
#nullable restore
#line 59 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        <p class=\"font-weight-bold\">\r\n            Tags\r\n        </p>\r\n        <p>\r\n            ");
#nullable restore
#line 65 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
       Write(Model.PostTags);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </p>\r\n        <hr>\r\n\r\n        <!-- Author -->\r\n        <div class=\"media mb-4\">\r\n            <img class=\"d-flex mr-3 rounded-circle\"");
            BeginWriteAttribute("src", " src=\"", 2278, "\"", 2341, 1);
#nullable restore
#line 71 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
WriteAttributeValue("", 2284, AccountHelper.GetAccountProfilePicture(Model.PostAuthor), 2284, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50\" height=\"50\"");
            BeginWriteAttribute("alt", " alt=\"", 2365, "\"", 2371, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"media-body\">\r\n                <p>\r\n                    Author\r\n                </p>\r\n                <h5 class=\"mt-0\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "479dcf27e068c4fc03ce7028a3627a3fb158a1ee14325", async() => {
                WriteLiteral("\r\n                        ");
#nullable restore
#line 78 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
                   Write(AccountHelper.GetAccountData(Model.PostAuthor, "FullName"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 77 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
                                                                                              WriteLiteral(Model.PostAuthor);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </h5>\r\n                ");
#nullable restore
#line 81 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\PostDetails\_VideoPostView.cshtml"
           Write(TextHelper.FormatLongText(TextHelper.StripHTML(AccountHelper.GetAccountData(Model.PostAuthor, "Biography")), 250));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.PostsDataModel.vwPostsModel> Html { get; private set; }
    }
}
#pragma warning restore 1591