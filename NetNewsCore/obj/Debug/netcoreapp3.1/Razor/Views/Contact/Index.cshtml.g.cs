#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c25a4bdc8f6958520032f75c025ec55dba004eb0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Contact_Index), @"mvc.1.0.view", @"/Views/Contact/Index.cshtml")]
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
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c25a4bdc8f6958520032f75c025ec55dba004eb0", @"/Views/Contact/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Contact_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NetNews.Models.AppDataModels.LoginViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_Head.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_BreakingNewsTop.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_LogoNav.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_Navigation.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/_ProcessMessage.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("fh5co_con_123"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("row"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("fh5co_contact_form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Contact", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "SendContact", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_13 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_FooterTop.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_14 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_FooterBottom.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_15 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "~/Views/Shared/Templates/24News/_FooterScripts.cshtml", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
  
    var Url = "https://" + Context.Request.Host + Context.Request.Path; //get current url path

    Layout = null;

    ViewBag.ActiveContact = "active"; //set active nav

#line default
#line hidden
#nullable disable
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"en\" class=\"no-js\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c25a4bdc8f6958520032f75c025ec55dba004eb010025", async() => {
                WriteLiteral("\r\n    <!-- 24 NEWS Head -->\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb010317", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c25a4bdc8f6958520032f75c025ec55dba004eb012202", async() => {
                WriteLiteral("\r\n    <div class=\"container-fluid fh5co_header_bg\">\r\n        <!-- Breaking News-->\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb012557", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n    <div class=\"container-fluid\">\r\n        <div class=\"container\">\r\n            <!-- Logo Navigation -->\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb013876", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"container-fluid bg-faded fh5co_padd_mediya padding_786\">\r\n        <div class=\"container padding_786\">\r\n            <!-- Navigation -->\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb015259", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n        <!-- Action messages partial -->\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb016506", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"container-fluid contact_us_bg_img\" style=\"background: #726e6e\">\r\n        <div class=\"container\">\r\n            <div class=\"row\">\r\n                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c25a4bdc8f6958520032f75c025ec55dba004eb017876", async() => {
                    WriteLiteral("<i class=\"fa fa-home\"></i>");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                <a href=""#"" class=""fh5co_con pt-2""> Contact Us </a>
            </div>
        </div>
    </div>
    <div class=""container-fluid  fh5co_fh5co_bg_contcat"">
        <div class=""container"">
            <div class=""row py-4"">
                <div class=""col-md-4 py-3"">
");
#nullable restore
#line 51 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                     if (!string.IsNullOrEmpty(ModelHelper.GetSiteLookupData("SiteNumber")))
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                        <div class=""row fh5co_contact_us_no_icon_difh5co_hover"">
                            <div class=""col-3 fh5co_contact_us_no_icon_difh5co_hover_1"">
                                <div class=""fh5co_contact_us_no_icon_div""> <span><i class=""fa fa-phone""></i></span> </div>
                            </div>
                            <div class=""col-9 align-self-center fh5co_contact_us_no_icon_difh5co_hover_2"">
                                <span class=""c_g d-block"">Call Us</span>
                                <span class=""d-block c_g fh5co_contact_us_no_text"">");
#nullable restore
#line 59 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                                                                              Write(ModelHelper.GetSiteLookupData("SiteNumber"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n                            </div>\r\n                            <div class=\"clearfix\"></div>\r\n                        </div>\r\n");
#nullable restore
#line 63 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </div>\r\n                <div class=\"col-md-4 py-3\">\r\n");
#nullable restore
#line 66 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                     if (!string.IsNullOrEmpty(ModelHelper.GetSiteLookupData("SiteEmail")))
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                        <div class=""row fh5co_contact_us_no_icon_difh5co_hover"">
                            <div class=""col-3 fh5co_contact_us_no_icon_difh5co_hover_1"">
                                <div class=""fh5co_contact_us_no_icon_div""> <span><i class=""fa fa-envelope""></i></span> </div>
                            </div>
                            <div class=""col-9 align-self-center fh5co_contact_us_no_icon_difh5co_hover_2"">
                                <span class=""c_g d-block"">Have any questions?</span>
                                <span class=""d-block c_g fh5co_contact_us_no_text"">");
#nullable restore
#line 74 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                                                                              Write(ModelHelper.GetSiteLookupData("SiteEmail"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n                            </div>\r\n                            <div class=\"clearfix\"></div>\r\n                        </div>\r\n");
#nullable restore
#line 78 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </div>\r\n                <div class=\"col-md-4 py-3\">\r\n");
#nullable restore
#line 81 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                     if (!string.IsNullOrEmpty(ModelHelper.GetSiteLookupData("SiteAddress")))
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                        <div class=""row fh5co_contact_us_no_icon_difh5co_hover"">
                            <div class=""col-3 fh5co_contact_us_no_icon_difh5co_hover_1"">
                                <div class=""fh5co_contact_us_no_icon_div""> <span><i class=""fa fa-map-marker""></i></span> </div>
                            </div>
                            <div class=""col-9 align-self-center fh5co_contact_us_no_icon_difh5co_hover_2"">
                                <span class=""c_g d-block"">Address</span>
                                <span class=""d-block c_g fh5co_contact_us_no_text""> ");
#nullable restore
#line 89 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                                                                               Write(ModelHelper.GetSiteLookupData("SiteAddress"));

#line default
#line hidden
#nullable disable
                WriteLiteral("</span>\r\n                            </div>\r\n                            <div class=\"clearfix\"></div>\r\n                        </div>\r\n");
#nullable restore
#line 93 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Contact\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                </div>
                <div class=""clearfix""></div>
            </div>
        </div>
    </div>
    <div class=""container-fluid mb-4"">
        <div class=""container"">
            <div class=""col-12 text-center contact_margin_svnit "">
                <div class=""text-center fh5co_heading py-2"">Contact Us</div>
            </div>
            <div class=""row"">
                <div class=""col-12 col-md-10 offset-md-1"" id=""NewPostForm"">
                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c25a4bdc8f6958520032f75c025ec55dba004eb025467", async() => {
                    WriteLiteral(@"
                        <div class=""col-12 py-3"">
                            <input type=""text"" class=""form-control fh5co_contact_text_box"" id=""Name"" name=""Name"" placeholder=""Enter Your Name"" required />
                        </div>
                        <div class=""col-6 py-3"">
                            <input type=""text"" class=""form-control fh5co_contact_text_box"" id=""Email"" name=""Email"" placeholder=""E-mail"" required />
                        </div>
                        <div class=""col-6 py-3"">
                            <input type=""text"" class=""form-control fh5co_contact_text_box"" id=""Subject"" name=""Subject"" placeholder=""Subject"" required />
                        </div>
                        <div class=""col-12 py-3"">
                            <textarea class=""form-control fh5co_contacts_message"" id=""Message"" name=""Message"" placeholder=""Message"" required></textarea>
                        </div>
                        <div class=""col-12 py-3 text-center"">
                ");
                    WriteLiteral("            <button type=\"submit\" class=\"btn contact_btn\">Send Message</button>\r\n                        </div>\r\n                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_11.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_12.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_12);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n\r\n\r\n\r\n    <div class=\"container-fluid fh5co_footer_bg pb-3\">\r\n        <div class=\"container animate-box\">\r\n            <!-- Footer Top -->\r\n            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb029068", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_13.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_13);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"container-fluid fh5co_footer_right_reserved\">\r\n        <!-- Footer Bottom -->\r\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb030388", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_14.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_14);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    </div>\r\n\r\n    <div class=\"gototop js-top\">\r\n        <a href=\"#\" class=\"js-gotop\"><i class=\"fa fa-arrow-up\"></i></a>\r\n    </div>\r\n\r\n    <!-- 24 NEWS Footer Scripts -->\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c25a4bdc8f6958520032f75c025ec55dba004eb031765", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_15.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_15);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NetNews.Models.AppDataModels.LoginViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591