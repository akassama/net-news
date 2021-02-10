using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models.AppModels
{
    public class SystemConfiguration
    {
        public string postTypes { get; set; }
        public string accountStatusDefinitions { get; set; }
        public string postStatusDefinitions { get; set; }
        public string connectionString { get; set; }
        public string smtpHost { get; set; }
        public string smtpEmail { get; set; }
        public string smtpPass { get; set; }
        public string emailDisplayName { get; set; }
        public int smtpPort { get; set; }
        public string emailClosure { get; set; }
        public string emailCompany { get; set; }
        public string emailUnsubscribeLink { get; set; }
        //
        public bool deleteApprovedPosts { get; set; }
        public bool editApprovedPosts { get; set; }
        public bool logSearches { get; set; }
        public bool logActivity { get; set; }
        public bool logSignIn { get; set; } 
        public bool showDonateLink { get; set; }
        public string visitLogTypes { get; set; }
        public string weatherLocationUrl { get; set; }
        public string weatherLocationText { get; set; }
        public string forexWidgetUrl { get; set; }
        public string covidWidgetClassId { get; set; }
    }
}
