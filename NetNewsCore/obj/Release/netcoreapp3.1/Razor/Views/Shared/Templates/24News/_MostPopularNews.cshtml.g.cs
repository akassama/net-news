#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_MostPopularNews.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "20f807aa56446d035671a290011240af416c9834"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Templates_24News__MostPopularNews), @"mvc.1.0.view", @"/Views/Shared/Templates/24News/_MostPopularNews.cshtml")]
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
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_MostPopularNews.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"20f807aa56446d035671a290011240af416c9834", @"/Views/Shared/Templates/24News/_MostPopularNews.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Templates_24News__MostPopularNews : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_MostPopularNews.cshtml"
 if (LayoutHelper24News.HasAnyTrendingNews())
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <div class=\"fh5co_heading fh5co_heading_border_bottom pt-3 py-2 mb-4\">Most Popular</div>\r\n    </div>\r\n    <span>\r\n        ");
#nullable restore
#line 9 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_MostPopularNews.cshtml"
   Write(LayoutHelper24News.GetPopularNews(4));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </span>\r\n");
#nullable restore
#line 11 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_MostPopularNews.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<!-- Adverts Section -->\r\n<div class=\"row\">\r\n    <div class=\"col-md-12\">\r\n\r\n    </div>\r\n</div>");
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
