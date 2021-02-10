#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3922542d30822e6fbfa99594a6537e8ead479a05"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout_Tables__AccountRoles), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/Tables/_AccountRoles.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3922542d30822e6fbfa99594a6537e8ead479a05", @"/Views/Shared/AccountLayout/Tables/_AccountRoles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout_Tables__AccountRoles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.AppDataModels.ChangePasswordModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
  
    string AccountID = HttpContextAccessor.HttpContext.Session.GetString("_AccountId");
    string ConnectionString = ViewBag.ConnectionString;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""container"">
    <h3>Admin Roles</h3>
    <div class=""row"">
        <div class=""col-sm-12"">
            <a href=""#AdminRoles"" data-toggle=""collapse"">
                <i class=""fas fa-chevron-circle-down"">
                    <span class=""text-danger"">Show</span>
                </i>
            </a>
        </div>
    </div>
    <div id=""AdminRoles"" class=""collapse  mb-3"">
        <div class=""form-check m-2"">
            <label class=""form-check-label"">
                <input type=""checkbox"" class=""form-check-input""");
            BeginWriteAttribute("value", " value=\"", 872, "\"", 880, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 25 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Accept Registrations", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Accept Registrations\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 1191, "\"", 1199, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 30 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Reject Registrations", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Reject Registrations\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 1510, "\"", 1518, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 35 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Suspend Accounts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Suspend Accounts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 1821, "\"", 1829, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 40 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Remove Accounts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Remove Accounts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 2130, "\"", 2138, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 45 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Edit Account Details", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Edit Account Details\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 2449, "\"", 2457, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 50 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Edit Account Permissions", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Edit Account Permissions\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 2776, "\"", 2784, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 55 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Manage Account Registrations", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Manage Account Registrations\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 3111, "\"", 3119, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 60 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Activate Accounts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" disabled> Activate Accounts
            </label>
        </div>
    </div>

    <hr />

    <h3>Editor Roles</h3>
    <div class=""row"">
        <div class=""col-sm-12"">
            <a href=""#EditorRoles"" data-toggle=""collapse"">
                <i class=""fas fa-chevron-circle-down"">
                    <span class=""text-danger"">Show</span>
                </i>
            </a>
        </div>
    </div>
    <div id=""EditorRoles"" class=""collapse  mb-3"">
        <div class=""form-check m-2"">
            <label class=""form-check-label"">
                <input type=""checkbox"" class=""form-check-input""");
            BeginWriteAttribute("value", " value=\"", 3829, "\"", 3837, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 80 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Accept Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Accept Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 4132, "\"", 4140, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 85 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Reject Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Reject Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 4435, "\"", 4443, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 90 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Comment Review on Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(@" disabled> Comment Review on Posts
            </label>
        </div>
    </div>


    <hr />

    <h3>Author Roles</h3>
    <div class=""row"">
        <div class=""col-sm-1"">
            <a href=""#AuthorRoles"" data-toggle=""collapse"">
                <i class=""fas fa-chevron-circle-down"">
                    <span class=""text-danger"">Show</span>
                </i>
            </a>
        </div>
    </div>
    <div id=""AuthorRoles"" class=""collapse  mb-3"">
        <div class=""form-check m-2"">
            <label class=""form-check-label"">
                <input type=""checkbox"" class=""form-check-input""");
            BeginWriteAttribute("value", " value=\"", 5166, "\"", 5174, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 111 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Create Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Create Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 5469, "\"", 5477, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 116 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Edit Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Edit Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 5768, "\"", 5776, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 121 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Edit Approved Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Edit Approved Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 6085, "\"", 6093, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 126 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Delete Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Delete Posts\r\n            </label>\r\n        </div>\r\n        <div class=\"form-check m-2\">\r\n            <label class=\"form-check-label\">\r\n                <input type=\"checkbox\" class=\"form-check-input\"");
            BeginWriteAttribute("value", " value=\"", 6388, "\"", 6396, 0);
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 131 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_AccountRoles.cshtml"
                                                                    Write(AccountHelper.CheckUserAccess(AccountID, "Delete Approved Posts", ViewBag.ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled> Delete Approved Posts\r\n            </label>\r\n        </div>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor HttpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.AppDataModels.ChangePasswordModel> Html { get; private set; }
    }
}
#pragma warning restore 1591