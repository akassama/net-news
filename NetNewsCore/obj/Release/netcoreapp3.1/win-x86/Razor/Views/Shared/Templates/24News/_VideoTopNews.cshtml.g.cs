#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_VideoTopNews.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6c7404f22737f2f434d707a274c445f3b8456930"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Templates_24News__VideoTopNews), @"mvc.1.0.view", @"/Views/Shared/Templates/24News/_VideoTopNews.cshtml")]
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
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_VideoTopNews.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6c7404f22737f2f434d707a274c445f3b8456930", @"/Views/Shared/Templates/24News/_VideoTopNews.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Templates_24News__VideoTopNews : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row mx-0\">\r\n    <div class=\"col-md-3\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_VideoTopNews.cshtml"
       Write(LayoutHelper24News.GetOtherFeaturedVideoNews(1, 2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-md-6 col-12 paddding animate-box\" data-animate-effect=\"fadeIn\">\r\n        ");
#nullable restore
#line 11 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_VideoTopNews.cshtml"
   Write(LayoutHelper24News.GetFeaturedVideoNews());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"col-md-3\">\r\n        <div class=\"row\">\r\n            ");
#nullable restore
#line 16 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_VideoTopNews.cshtml"
       Write(LayoutHelper24News.GetOtherFeaturedVideoNews(3, 2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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