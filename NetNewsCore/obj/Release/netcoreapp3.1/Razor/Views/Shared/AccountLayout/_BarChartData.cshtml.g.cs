#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a89b8bde108d2032d29b5b020ae1069739f7f1b1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout__BarChartData), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/_BarChartData.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a89b8bde108d2032d29b5b020ae1069739f7f1b1", @"/Views/Shared/AccountLayout/_BarChartData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout__BarChartData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NetNews.Models.PostsDataModel.vwPostsApprovedModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
  
    string AccountID = HttpContextAccessor.HttpContext.Session.GetString("_AccountId");
    string ConnectionString = ViewBag.ConnectionString;
    int Year = DateTime.Now.Year;
    if (!string.IsNullOrEmpty(ViewBag.Year))
    {
        Year = DataHelper.Int32Parse(ViewBag.Year, DateTime.Now.Year);
    }

    //get max value
    int[] viewsArray = {    DataHelper.GetTotalViewsPerMonth(AccountID, 01, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 02, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 03, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 04, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 05, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 06, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 07, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 08, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 09, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 10, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 11, Year, ConnectionString),
                            DataHelper.GetTotalViewsPerMonth(AccountID, 12, Year, ConnectionString),
        };
    int maxValue = viewsArray.Max();
    int maxIndex = viewsArray.ToList().IndexOf(maxValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script>
    // Set new default font family and font color to mimic Bootstrap's default styling
    Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif';
    Chart.defaults.global.defaultFontColor = '#292b2c';

    // Bar Chart Example
    var ctx = document.getElementById(""myBarChart"");
    var myLineChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: [""January"", ""February"", ""March"", ""April"", ""May"", ""June"", ""July"", ""August"", ""September"", ""October"", ""November"", ""December""],
            datasets: [{
                label: ""Total Views"",
                backgroundColor: ""rgba(2,117,216,1)"",
                borderColor: ""rgba(2,117,216,1)"",
                data: [
						");
#nullable restore
#line 49 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 01, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 50 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 02, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 51 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 03, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 52 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 04, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 05, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 54 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 06, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 55 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 07, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 56 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 08, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 57 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 09, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 58 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 10, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 59 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 11, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 60 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 12, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                    ],
            }],
        },
        options: {
            scales: {
                xAxes: [{
                    time: {
                        unit: 'month'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 12
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: ");
#nullable restore
#line 80 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_BarChartData.cshtml"
                        Write(maxValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                        maxTicksLimit: 6
                    },
                    gridLines: {
                        display: true
                    }
                }],
            },
            legend: {
                display: false
            }
        }
    });

</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<NetNews.Models.PostsDataModel.vwPostsApprovedModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
