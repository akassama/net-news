#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eca0eae81ab9a67913364e72fa9cd66a505fa37b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout__LineChartDaysData), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/_LineChartDaysData.cshtml")]
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
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eca0eae81ab9a67913364e72fa9cd66a505fa37b", @"/Views/Shared/AccountLayout/_LineChartDaysData.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout__LineChartDaysData : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<NetNews.Models.PostsDataModel.vwPostsApprovedModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 6 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
  
    string AccountID = HttpContextAccessor.HttpContext.Session.GetString("_AccountId");
    string ConnectionString = ViewBag.ConnectionString;
    int Year = DateTime.Now.Year;
    if (!string.IsNullOrEmpty(ViewBag.Year))
    {
        Year = DataHelper.Int32Parse(ViewBag.Year, DateTime.Now.Year);
    }

    int Month = DateTime.Now.Month;

    //get max value
    int[] viewsArray = {
                        DataHelper.GetTotalViewsPerDay(AccountID, -9, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -8, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -7, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -6, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -5, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -4, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -3, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -2, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, -1, Month, Year, ConnectionString),
                        DataHelper.GetTotalViewsPerDay(AccountID, 0, Month, Year, ConnectionString)
        };
    int maxValue = viewsArray.Max();
    int maxIndex = DataHelper.Int32Parse(viewsArray.ToList().IndexOf(maxValue).ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    <script>
    // Set new default font family and font color to mimic Bootstrap's default styling
    Chart.defaults.global.defaultFontFamily = '-apple-system,system-ui,BlinkMacSystemFont,""Segoe UI"",Roboto,""Helvetica Neue"",Arial,sans-serif';
    Chart.defaults.global.defaultFontColor = '#292b2c';

    // Area Chart Example
    var ctx = document.getElementById(""myAreaChart"");
    var myLineChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [""");
#nullable restore
#line 44 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                 Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 44 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                                Write(DateTime.Now.AddDays(-9).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 45 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 45 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-8).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 46 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 46 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-7).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 47 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-6).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 48 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 48 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-5).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 49 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 49 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-4).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 50 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 50 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-3).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 51 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 51 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-2).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 52 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 52 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(-1).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral("\",\r\n                    \"");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                Write(DateTime.Now.ToString("MMMM"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 53 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                                               Write(DateTime.Now.AddDays(0).Day);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"""],
            datasets: [{
                label: ""Views"",
                lineTension: 0.3,
                backgroundColor: ""rgba(2,117,216,0.2)"",
                borderColor: ""rgba(2,117,216,1)"",
                pointRadius: 5,
                pointBackgroundColor: ""rgba(2,117,216,1)"",
                pointBorderColor: ""rgba(255,255,255,0.8)"",
                pointHoverRadius: 5,
                pointHoverBackgroundColor: ""rgba(2,117,216,1)"",
                pointHitRadius: 50,
                pointBorderWidth: 2,
                data: [
						");
#nullable restore
#line 67 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -9, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 68 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -8, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 69 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -7, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 70 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -6, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 71 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -5, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 72 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -4, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 73 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -3, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 74 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -2, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 75 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, -1, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n                        ");
#nullable restore
#line 76 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                   Write(DataHelper.GetTotalViewsPerDay(AccountID, 0, Month, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                ],
            }],
        },
        options: {
            scales: {
                xAxes: [{
                    time: {
                        unit: 'date'
                    },
                    gridLines: {
                        display: false
                    },
                    ticks: {
                        maxTicksLimit: 10
                    }
                }],
                yAxes: [{
                    ticks: {
                        min: 0,
                        max: ");
#nullable restore
#line 96 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNewsCore\NetNewsCore\Views\Shared\AccountLayout\_LineChartDaysData.cshtml"
                        Write(maxValue);

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
                        maxTicksLimit: 5
                    },
                    gridLines: {
                        color: ""rgba(0, 0, 0, .125)"",
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
