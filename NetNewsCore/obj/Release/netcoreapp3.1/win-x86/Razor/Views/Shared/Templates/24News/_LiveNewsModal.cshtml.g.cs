#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_LiveNewsModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "19c74c6f6bd1d58deb0304c99c9753b7efcd0fee"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Templates_24News__LiveNewsModal), @"mvc.1.0.view", @"/Views/Shared/Templates/24News/_LiveNewsModal.cshtml")]
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
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_LiveNewsModal.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"19c74c6f6bd1d58deb0304c99c9753b7efcd0fee", @"/Views/Shared/Templates/24News/_LiveNewsModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Templates_24News__LiveNewsModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""row"">
    <div class=""col-sm-12 col-md-2 offset-md-10"">
        <span class=""float-right"">
            <a role=""button"" class=""btn btn-danger"" data-toggle=""modal"" data-target=""#liveNewsModal"">
                <i class=""fab fa-youtube text-white""></i> <span class=""text-white"">Live News</span>
            </a>
        </span>
    </div>
</div>

<script type=""text/javascript"">
    //Stop YouTube Player
    $(document).ready(function () {
        $("".close-yt-player"").on('click', function (event) {
            var video = $(""#live-yt-player"").attr(""src"");
            $(""#live-yt-player"").attr(""src"", """");
            $(""#live-yt-player"").attr(""src"", video);
        });
    });
</script>

<!-- Live News Modal -->
<div class=""modal fade"" id=""liveNewsModal"">
    <div class=""modal-dialog modal-lg"">
        <div class=""modal-content"">

            <!-- Modal Header -->
            <div class=""modal-header"">
                <h4 class=""modal-title""><i class=""fab fa-youtube text-dan");
            WriteLiteral(@"ger""></i> <span>Live News</span></h4>
                <button type=""button"" class=""close close-yt-player"" data-dismiss=""modal"">&times;</button>
            </div>

            <!-- Modal body -->
            <div class=""modal-body"">
                <div class=""embed-responsive embed-responsive-16by9"">
                    <iframe class=""embed-responsive-item"" id=""live-yt-player""");
            BeginWriteAttribute("src", " src=\"", 1439, "\"", 1519, 2);
            WriteAttributeValue("", 1445, "https://www.youtube.com/embed/", 1445, 30, true);
#nullable restore
#line 37 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\Templates\24News\_LiveNewsModal.cshtml"
WriteAttributeValue("", 1475, ModelHelper.GetSiteLookupData("LiveNewsID"), 1475, 44, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" allowfullscreen></iframe>
                </div>
            </div>

            <!-- Modal footer -->
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger close-yt-player"" data-dismiss=""modal"">Close</button>
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