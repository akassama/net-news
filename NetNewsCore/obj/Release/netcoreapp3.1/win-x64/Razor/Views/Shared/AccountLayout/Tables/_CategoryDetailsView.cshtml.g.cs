#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "71d83cfc1be793a2a6909c3ec8af4a7caedf4a15"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout_Tables__CategoryDetailsView), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/Tables/_CategoryDetailsView.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"71d83cfc1be793a2a6909c3ec8af4a7caedf4a15", @"/Views/Shared/AccountLayout/Tables/_CategoryDetailsView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout_Tables__CategoryDetailsView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.PostsDataModel.CategoriesModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"
<div class=""col-lg-9 stretch-card grid-margin"">
    <div class=""card"">
        <div class=""card-body"">
            <div class=""row"">
                <div class=""col main pt-0 mt-3"">
                    <p class=""lead"">Category Details</p>
                    <span class=""float-right"">");
#nullable restore
#line 10 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                         Write(TextHelper.FormatPublishedState(Model.IsPublished));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    <div class=\"row mb-3\">\r\n                        <ul class=\"list-group\">\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 13 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                   Write(Html.DisplayNameFor(model => model.CategoryName));

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 13 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                                                                       Write(Html.DisplayFor(model => model.CategoryName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 14 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                   Write(Html.DisplayNameFor(model => model.CategoryParent));

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 14 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                                                                         Write(PostHelper.GetCategoryParent(Model.CategoryParent));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 15 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                   Write(Html.DisplayNameFor(model => model.CategoryDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 15 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                                                                              Write(Html.DisplayFor(model => model.CategoryDescription));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 16 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                   Write(Html.DisplayNameFor(model => model.IsHeader));

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 16 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                                                                   Write(TextHelper.FormatHeaderState(Model.IsHeader));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                            <li class=\"list-group-item\">");
#nullable restore
#line 17 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                   Write(Html.DisplayNameFor(model => model.DateAdded));

#line default
#line hidden
#nullable disable
            WriteLiteral(" : ");
#nullable restore
#line 17 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_CategoryDetailsView.cshtml"
                                                                                                    Write(Html.DisplayFor(model => model.DateAdded));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\r\n                        </ul>\r\n                    </div>\r\n                </div>\r\n                <!--/main col-->\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.PostsDataModel.CategoriesModel> Html { get; private set; }
    }
}
#pragma warning restore 1591