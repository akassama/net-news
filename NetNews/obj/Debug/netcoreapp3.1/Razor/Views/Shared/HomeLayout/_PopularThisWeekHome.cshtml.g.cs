#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\HomeLayout\_PopularThisWeekHome.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f810441d930e550b88d24fdfadb885ce8c6b5259"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_HomeLayout__PopularThisWeekHome), @"mvc.1.0.view", @"/Views/Shared/HomeLayout/_PopularThisWeekHome.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f810441d930e550b88d24fdfadb885ce8c6b5259", @"/Views/Shared/HomeLayout/_PopularThisWeekHome.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"865daf883d7bb03d997f2023dfb26fba4ca7e931", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_HomeLayout__PopularThisWeekHome : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/images/gambia/grant.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString("thumb"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("img-fluid rounded"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!-- Popular This Week -->\r\n<div class=\"col-xl-6\">\r\n    <div class=\"card-title\">\r\n        Popular this week\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-xl-6 col-lg-8 col-sm-6\">\r\n            <div class=\"rotate-img\">\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f810441d930e550b88d24fdfadb885ce8c6b52594609", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
            </div>
            <h2 class=""mt-3 text-primary mb-2"">
                <a href=""pages/single-post.html"" class=""text-primary text-decoration-none"">
                    The 15 Million Grant
                </a>
            </h2>
            <p class=""fs-13 mb-1 text-muted"">
                <span class=""mr-2"">Photo </span>10 Minutes ago
            </p>
            <p class=""my-3 fs-15"">
                The Ministry of information is engaged in consultation with GPU and the Newspaper Publishers Association on how to make the disbursement of the grant...
            </p>
            <a href=""#"" class=""font-weight-600 fs-16 text-dark"">Read more</a>
        </div>
        <div class=""col-xl-6 col-lg-4 col-sm-6"">
            <div class=""border-bottom pb-3 mb-3"">
                <h3 class=""font-weight-600 mb-0"">
                    <a href=""pages/single-post.html"" class=""text-dark text-decoration-none"">
                        Social distancing is ..
                    </a>
        ");
            WriteLiteral(@"        </h3>
                <p class=""fs-13 text-muted mb-0"">
                    <span class=""mr-2"">Photo </span>10 Minutes ago
                </p>
                <p class=""mb-0"">
                    Social distancing, also called “physical distancing,” means keeping a safe space...
                </p>
            </div>
            <div class=""border-bottom pb-3 mb-3"">
                <h3 class=""font-weight-600 mb-0"">
                    <a href=""pages/single-post.html"" class=""text-dark text-decoration-none"">
                        Panic buying is forcing..
                    </a>
                </h3>
                <p class=""fs-13 text-muted mb-0"">
                    <span class=""mr-2"">Photo </span>10 Minutes ago
                </p>
                <p class=""mb-0"">
                    Panic buying is forcing supermarkets to ration food and other supplies....
                </p>
            </div>
            <div class=""border-bottom pb-3 mb-3"">
                <h3 class=""");
            WriteLiteral(@"font-weight-600 mb-0"">
                    <a href=""pages/single-post.html"" class=""text-dark text-decoration-none"">
                        Furious transport union president...
                    </a>
                </h3>
                <p class=""fs-13 text-muted mb-0"">
                    <span class=""mr-2"">Photo </span>10 Minutes ago
                </p>
                <p class=""mb-0"">
                    The president of The Gambia National Transport Union, Omar Ceesay, has...
                </p>
            </div>
            <div>
                <h3 class=""font-weight-600 mb-0"">
                    <a href=""pages/single-post.html"" class=""text-dark text-decoration-none"">
                        Freelance journalists assaulted at Aqua Gambia
                    </a>
                </h3>
                <p class=""fs-13 text-muted mb-0"">
                    <span class=""mr-2"">Photo </span>10 Minutes ago
                </p>
                <p class=""mb-0"">
                    Sec");
            WriteLiteral("urity officer at Aqua Gambia has assaulted 2 freelance journalists at the...\r\n                </p>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
