#pragma checksum "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51326b6f58eae1285d68372c60f9e64a5b682b91"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_AccountLayout_Tables__PostsViewsLineChart___Copy), @"mvc.1.0.view", @"/Views/Shared/AccountLayout/Tables/_PostsViewsLineChart - Copy.cshtml")]
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
#line 1 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
using AppHelpers.App_Code;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51326b6f58eae1285d68372c60f9e64a5b682b91", @"/Views/Shared/AccountLayout/Tables/_PostsViewsLineChart - Copy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"daaccc913fa25243b66b86b29fe8e3e461539d55", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_AccountLayout_Tables__PostsViewsLineChart___Copy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 5 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
  
    string AccountID = HttpContextAccessor.HttpContext.Session.GetString("_AccountId");
    string ConnectionString = ViewBag.ConnectionString;
    int Year = DateTime.Now.Year;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""col-12"">
    <!-- Content Row -->
    <div class=""row"">
        <div class=""col-xl-6 col-lg-6 col-md-12 col-sm-12"">
            <!-- Area Chart -->
            <div class=""card shadow mb-4"">
                <div class=""card-header py-3"">
                    <h6 class=""m-0 font-weight-bold text-primary"">Post Views</h6>
                </div>
                <div class=""card-body"">
                    <div style=""width:100%;"">
                        <canvas id=""line-chart""></canvas>
                    </div>
                    <hr>

                </div>
            </div>
        </div>

        <div class=""col-xl-6 col-lg-6 col-md-12 col-sm-12"">
            <!-- Bar Chart -->
            <div class=""card shadow mb-4"">
                <div class=""card-header py-3"">
                    <h6 class=""m-0 font-weight-bold text-primary"">Posts Per Month</h6>
                </div>
                <div class=""card-body"">
                    <div style=""width:100%;"">
        ");
            WriteLiteral(@"                <canvas id=""bar-chart""></canvas>
                    </div>
                    <hr>

                </div>
            </div>
        </div>
    </div>
</div>


<script>
		var MONTHS = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
		var config = {
			type: 'line',
			data: {
				labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
				datasets: [{
					label: 'View graph',
					backgroundColor: window.chartColors.red,
					borderColor: window.chartColors.red,
					data: [
						");
#nullable restore
#line 60 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 01, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 61 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 02, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 62 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 03, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 63 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 04, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 64 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 05, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 65 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 06, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 66 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 07, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 67 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 08, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 68 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 09, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 69 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 10, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 70 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 11, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(",\r\n\t\t\t\t\t\t");
#nullable restore
#line 71 "C:\Users\Laiman\Documents\Visual Studio 2019\Projects\NetNews\NetNews\Views\Shared\AccountLayout\Tables\_PostsViewsLineChart - Copy.cshtml"
                   Write(DataHelper.GetTotalViewsPerMonth(AccountID, 12, Year, ConnectionString));

#line default
#line hidden
#nullable disable
            WriteLiteral(@",
					],
					fill: false,
				}]
			},
			options: {
				responsive: true,
				title: {
					display: true,
					text: 'Views per month'
				},
				tooltips: {
					mode: 'index',
					intersect: false,
				},
				hover: {
					mode: 'nearest',
					intersect: true
				},
				scales: {
					xAxes: [{
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Month'
						}
					}],
					yAxes: [{
						display: true,
						scaleLabel: {
							display: true,
							labelString: 'Total Views'
						}
					}]
				}
			}
		};

		window.onload = function() {
			var ctx = document.getElementById('line-chart').getContext('2d');
			window.myLine = new Chart(ctx, config);
		};

		var colorNames = Object.keys(window.chartColors);

</script>
");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
