#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea5b71a38a36cf496c307eb5372bd073ac310a6d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Templates_24News_PostDetails__NewsAudioPostView), @"mvc.1.0.view", @"/Views/Shared/Templates/24News/PostDetails/_NewsAudioPostView.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea5b71a38a36cf496c307eb5372bd073ac310a6d", @"/Views/Shared/Templates/24News/PostDetails/_NewsAudioPostView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Templates_24News_PostDetails__NewsAudioPostView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.PostsDataModel.vwPostsApprovedModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString("_blank"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "RedirectAuthor", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Authors", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("audio/mpeg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration-none text-dark"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
  
    string AudioDirectory = DateTime.Parse(Model.DateAdded.ToString()).ToString("MM-yyyy");

    string link = Context.Request.Host + Context.Request.Path;
    var post_url = (link.Contains("http")) ? link : "https://" + link;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div class=\"col-lg-12\">\r\n\r\n        <!-- Title -->\r\n        <h1 class=\"mt-4\">\r\n            ");
#nullable restore
#line 15 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
       Write(Html.DisplayFor(model => model.PostTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </h1>\r\n\r\n        <!-- Author -->\r\n        <p class=\"lead mt-2\">\r\n            by\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea5b71a38a36cf496c307eb5372bd073ac310a6d6518", async() => {
                WriteLiteral("\r\n                ");
#nullable restore
#line 22 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
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
#line 21 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
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
            WriteLiteral("\r\n\r\n            <span class=\"float-right\" style=\"font-size: 0.7em;\">\r\n                <i class=\"fas fa-eye\"></i>\r\n                ");
#nullable restore
#line 27 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
           Write(PostHelper.GetPostViews(Model.PostID));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </span>
        </p>

        <hr>

        <!-- Preview Image -->
        <div class=""row justify-content-center"">
            <div class=""col-sm-12 col-md-10 mb-2"">
                <div class=""card zoom-xm"">
                    <div class=""card-body"">
                        <h5 class=""card-title p-1""><i class=""fas fa-music mr-2""></i>");
#nullable restore
#line 38 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
                                                                               Write(Model.PostTitle);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <div class=\"bg-secondary border border-dark rounded pt-2 mb-2 text-center\">\r\n                            <audio controls controlsList=\"nodownload\">\r\n                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("source", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ea5b71a38a36cf496c307eb5372bd073ac310a6d10864", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "src", 4, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 1539, "~/files/audios/", 1539, 15, true);
#nullable restore
#line 41 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
AddHtmlAttributeValue("", 1554, AudioDirectory, 1554, 15, false);

#line default
#line hidden
#nullable disable
            AddHtmlAttributeValue("", 1569, "/", 1569, 1, true);
#nullable restore
#line 41 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
AddHtmlAttributeValue("", 1570, Model.PostAudioLink, 1570, 20, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                Your browser does not support the audio element.
                            </audio>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <hr>
        <!-- Date/Time -->
        <p>
            ");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
       Write(PostHelper.FormatPostDate(Model.DateAdded.ToString()));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n            <span class=\"float-right\">\r\n                <small>Editor: ");
#nullable restore
#line 56 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
                          Write(AccountHelper.GetAccountData(Model.PostEditor, "FullName"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</small>\r\n            </span>\r\n        </p>\r\n\r\n        <hr>\r\n\r\n        <!-- Post Content -->\r\n");
#nullable restore
#line 63 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
         if (!string.IsNullOrEmpty(Model.PostContent))
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div>\r\n                ");
#nullable restore
#line 66 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
           Write(Html.Raw(Model.PostContent));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n            <hr>\r\n");
#nullable restore
#line 69 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n        <!-- Author -->\r\n        <div class=\"media mb-4\">\r\n            <img class=\"d-flex mr-3 rounded-circle\"");
            BeginWriteAttribute("src", " src=\"", 2498, "\"", 2561, 1);
#nullable restore
#line 74 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
WriteAttributeValue("", 2504, AccountHelper.GetAccountProfilePicture(Model.PostAuthor), 2504, 57, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50\" height=\"50\"");
            BeginWriteAttribute("alt", " alt=\"", 2585, "\"", 2591, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n            <div class=\"media-body\">\r\n                <p>\r\n                    Author\r\n                </p>\r\n                <h5 class=\"mt-0\">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ea5b71a38a36cf496c307eb5372bd073ac310a6d15981", async() => {
                WriteLiteral("\r\n                        ");
#nullable restore
#line 81 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
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
#line 80 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
                                                                                              WriteLiteral(Model.PostAuthor);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </h5>\r\n                ");
#nullable restore
#line 84 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
           Write(TextHelper.FormatLongText(TextHelper.StripHTML(AccountHelper.GetAccountData(Model.PostAuthor, "Biography")), 250));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </div>
        </div>


        <hr>
        <!-- Facebook Comments -->
        <div class=""card my-4 mt-5"">
            <h5 class=""card-header"">Comments</h5>
            <div class=""card-body"">
                <div class=""fb-comments"" data-href=""");
#nullable restore
#line 94 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\PostDetails\_NewsAudioPostView.cshtml"
                                               Write(post_url);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" data-numposts=\"10\" data-mobile=\"true\" data-width=\"\"></div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.PostsDataModel.vwPostsApprovedModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
