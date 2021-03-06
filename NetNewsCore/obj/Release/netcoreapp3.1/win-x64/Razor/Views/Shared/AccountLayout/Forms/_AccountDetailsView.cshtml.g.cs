#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cc668eead242225f0d0f3002a7cdc35d06045507"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout_Forms__AccountDetailsView), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/Forms/_AccountDetailsView.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cc668eead242225f0d0f3002a7cdc35d06045507", @"/Views/Shared/AccountLayout/Forms/_AccountDetailsView.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout_Forms__AccountDetailsView : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.AccountsDataModel.AccountsModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
  
    string AccountID = HttpContextAccessor.HttpContext.Session.GetString("_AccountId");
    string ConnectionString = ViewBag.ConnectionString;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col-lg-9 stretch-card grid-margin"">
    <div class=""card"">
        <div class=""card-body"">
            <div class=""row"">
                <div class=""col main pt-0 mt-3"">
                    <p class=""lead"">Account Details</p>
                    <span class=""float-right"">");
#nullable restore
#line 17 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                         Write(AccountHelper.GetAccountApprovalState(Model.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    <div class=\"row mb-3\">\r\n                        <p>\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 780, "\"", 842, 1);
#nullable restore
#line 20 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 786, AccountHelper.GetAccountProfilePicture(Model.AccountID), 786, 56, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" class=""rounded"" alt=""account profile picture"" width=""120"" height=""120"">
                        </p>
                        <div class=""row"">
                            <div class=""col-sm-12 col-md-6 col-lg-3 col-xl-6 mb-2"">
                                <label for=""FirstName"">First Name:</label>
");
#nullable restore
#line 25 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                   var FirstName = ModelHelper.SetEditInput(TempData["FirstName"], AccountHelper.GetAccountData(AccountID, "FirstName")); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input type=\"text\" class=\"form-control\" maxlength=\"100\"");
            BeginWriteAttribute("value", " value=\"", 1395, "\"", 1413, 1);
#nullable restore
#line 26 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 1403, FirstName, 1403, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 26 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                                      Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled>\r\n                            </div>\r\n                            <div class=\"col-sm-12 col-md-6 col-lg-3 col-xl-6 mb-2\">\r\n                                <label for=\"LastName\">Last Name:</label>\r\n");
#nullable restore
#line 30 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                   var LastName = ModelHelper.SetEditInput(TempData["LastName"], AccountHelper.GetAccountData(AccountID, "LastName")); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input type=\"text\" class=\"form-control\" maxlength=\"100\"");
            BeginWriteAttribute("value", " value=\"", 1885, "\"", 1902, 1);
#nullable restore
#line 31 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 1893, LastName, 1893, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 31 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                                     Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled>\r\n                            </div>\r\n                            <div class=\"col-sm-12 col-md-6 col-lg-3 col-xl-6 mb-2\">\r\n                                <label for=\"Email\">Email:</label>\r\n");
#nullable restore
#line 35 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                   var Email = ModelHelper.SetEditInput(TempData["Email"], AccountHelper.GetAccountData(AccountID, "Email")); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input type=\"email\" class=\"form-control\" maxlength=\"100\"");
            BeginWriteAttribute("value", " value=\"", 2359, "\"", 2373, 1);
#nullable restore
#line 36 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 2367, Email, 2367, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("  ");
#nullable restore
#line 36 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                                    Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" disabled>
                            </div>
                            <div class=""col-sm-12 col-md-6 col-lg-4 col-xl-6 mb-2"">
                                <label for=""Num"">Mobile Number:</label>
                                <div class=""row"">
                                    <div class=""col-4"">
");
#nullable restore
#line 42 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                           var CountryCode = ModelHelper.SetEditInput(TempData["CountryCode"], AccountHelper.GetAccountData(AccountID, "CountryCode")); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <input type=\"text\" class=\"form-control integer-only\" maxlength=\"5\"");
            BeginWriteAttribute("value", " value=\"", 2989, "\"", 3009, 1);
#nullable restore
#line 43 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 2997, CountryCode, 2997, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 43 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                                                           Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled>\r\n                                    </div>\r\n                                    <div class=\"col-8\">\r\n");
#nullable restore
#line 46 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                           var PhoneNumber = ModelHelper.SetEditInput(TempData["PhoneNumber"], AccountHelper.GetAccountData(AccountID, "PhoneNumber")); 

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <input type=\"text\" class=\"form-control integer-only\" maxlength=\"20\"");
            BeginWriteAttribute("value", " value=\"", 3424, "\"", 3444, 1);
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 3432, PhoneNumber, 3432, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                                                            Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" disabled>
                                    </div>
                                </div>
                            </div>
                            <div class=""col-sm-12 col-md-6 col-lg-3 col-xl-6 mb-2"">
                                <label for=""DateOfBirth"">Date of Birth:</label>
");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                  
                                    var DateOfBirth = ModelHelper.SetEditInput(TempData["DateOfBirth"], AccountHelper.GetAccountData(AccountID, "DateOfBirth"));
                                    DateOfBirth = Convert.ToDateTime(DateOfBirth).ToString("dd/MM/yyyy");

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <input type=\"text\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 4146, "\"", 4166, 1);
#nullable restore
#line 56 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 4154, DateOfBirth, 4154, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" ");
#nullable restore
#line 56 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                                            Write(ViewBag.ProfileInputs);

#line default
#line hidden
#nullable disable
            WriteLiteral(" disabled>\r\n");
            WriteLiteral("                            </div>\r\n                            <div class=\"col-sm-12 col-md-6 col-lg-3 col-xl-6 mb-2\">\r\n                                <label for=\"Gender\">Gender:</label>\r\n");
#nullable restore
#line 61 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                  
                                    var Gender = ModelHelper.GetTableData("AccountDetails", "AccountID", AccountID, "Gender", ConnectionString);
                                    string selected_value = "";
                                    if (Gender == "Male")
                                    {
                                        selected_value = "Male";
                                    }
                                    else if (Gender == "Female")
                                    {
                                        selected_value = "Female";
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <input type=\"text\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 5164, "\"", 5187, 1);
#nullable restore
#line 72 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 5172, selected_value, 5172, 15, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" disabled>\r\n");
            WriteLiteral(@"                            </div>
                            <div class=""col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-2"">
                                <label for=""Country"">
                                    Nationality:
                                </label>
");
#nullable restore
#line 79 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                  

                                    var Country = ModelHelper.SetEditInput(TempData["Country"], AccountHelper.GetAccountData(AccountID, "Country"));
                                    var CountryName = ModelHelper.GetTableData("Country", "ID", Country, "Name", ViewBag.ConnectionString);
                                    var CountryNameOption = (!string.IsNullOrEmpty(CountryName)) ? "<option value='" + Country + "' class='text-white bg-dark' selected>" + CountryName + "</option>" : "<option value='' class='text-white bg-dark'>Select country</option>";

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <input type=\"text\" class=\"form-control\"");
            BeginWriteAttribute("value", " value=\"", 6182, "\"", 6202, 1);
#nullable restore
#line 84 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
WriteAttributeValue("", 6190, CountryName, 6190, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" disabled>\r\n");
            WriteLiteral("                            </div>\r\n                            <div class=\"col-sm-12 col-md-12 col-lg-12 col-xl-12 mb-2\">\r\n                                <label for=\"DateOfBirth\">Biography:</label>\r\n");
#nullable restore
#line 89 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                  
                                    var Biography = ModelHelper.SetEditInput(TempData["Biography"], AccountHelper.GetAccountData(AccountID, "Biography"));

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <div class=\"bg-light text-dark rounded p-3\">");
#nullable restore
#line 91 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Forms\_AccountDetailsView.cshtml"
                                                                           Write(Html.Raw(Biography));

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n");
            WriteLiteral("                            </div>\r\n                        </div>\r\n\r\n                    </div>\r\n                    <!--/row-->\r\n                </div>\r\n                <!--/main col-->\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.AccountsDataModel.AccountsModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
