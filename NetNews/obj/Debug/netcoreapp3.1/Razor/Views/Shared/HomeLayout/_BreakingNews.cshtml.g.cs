#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\HomeLayout\_BreakingNews.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e52db78564865bc40b4b710497b2b261a902d222"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_HomeLayout__BreakingNews), @"mvc.1.0.view", @"/Views/Shared/HomeLayout/_BreakingNews.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e52db78564865bc40b4b710497b2b261a902d222", @"/Views/Shared/HomeLayout/_BreakingNews.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"865daf883d7bb03d997f2023dfb26fba4ca7e931", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_HomeLayout__BreakingNews : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Breaking News -->
<div class=""flash-news-banner"">
    <div class=""container"">
        <div class=""d-lg-flex align-items-center justify-content-between"">
            <div class=""d-flex align-items-center"">
                <span class=""badge badge-danger mr-3"">Breaking news</span>
                <p class=""mb-0"">
                    <a href=""#"" class=""text-decoration-none text-dark"">
                        Lorem Ipsum has been the industry's standard dummy text ever since the 1500s.
                    </a>
                </p>
            </div>
            <div class=""d-flex"">
");
            WriteLiteral(@"                <div class=""input-group mb-1"">
                    <input type=""text"" class=""form-control"" placeholder=""Search here..."">
                    <div class=""input-group-append"">
                        <button class=""btn btn-primary border border-white"" type=""submit"">
                            <i class=""mdi mdi-magnify""></i>
                        </button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>");
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
