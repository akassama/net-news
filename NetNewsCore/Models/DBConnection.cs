using Microsoft.EntityFrameworkCore;
using NetNews.Models.AccessDataModels;
using NetNews.Models.AccountsDataModel;
using NetNews.Models.AppDataModels;
using NetNews.Models.PostsDataModel;
using NetNews.Models.SubscriptionsDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetNews.Models
{
    public class DBConnection : DbContext
    {

        public DBConnection()
        {
        }

        public DBConnection(DbContextOptions<DBConnection> options) : base(options)
        {
        }

        //Account Models
        public DbSet<AccountsModel> Accounts { get; set; }
        public DbSet<AccountDetailsModel> AccountDetails { get; set; }
        public DbSet<SocialMediaDetailsModel> SocialMediaDetails { get; set; }


        //App Models
        public DbSet<LoginViewModel> LoginView { get; set; }
        public DbSet<SiteDataLookupModel> SiteDataLookup { get; set; }
        public DbSet<ActivityLogsModel> ActivityLogs { get; set; }
        public DbSet<ContactMessagesModel> ContactMessages { get; set; }
        public DbSet<MessageViewsModel> MessageViews { get; set; }
        public DbSet<CountryModel> Countries { get; set; }
        public DbSet<PasswordForgotModel> PasswordForgot { get; set; }
        public DbSet<AdvertsModel> Adverts { get; set; }
        public DbSet<JobsModel> Jobs { get; set; }
        public DbSet<SiteStatsModel> SiteStats { get; set; }
        public DbSet<VisitLogsModel> VisitLogs { get; set; }


        //Access Data Models
        public DbSet<PermissionsModel> Permissions { get; set; }
        public DbSet<AccountToPermissionModel> AccountToPermission { get; set; }


        //Posts Models
        public DbSet<PostsModel> Posts { get; set; }
        public DbSet<NewsApiModel> NewsApi { get; set; }
        public DbSet<vwPostsModel> vwPosts { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<TagsModel> Tags { get; set; }
        public DbSet<GalleryImagesModel> GalleryImages { get; set; }
        public DbSet<VideoUploadsModel> VideoUploads { get; set; }
        public DbSet<PostApprovalsModel> PostApprovals { get; set; }
        public DbSet<PostViewsModel> PostViews { get; set; }
        public DbSet<vwPostReviewsModel> vwPostReviews { get; set; }
        public DbSet<PostReviewsModel> PostReviews { get; set; }
        public DbSet<vwPostsApprovedModel> vwPostsApproved { get; set; }
        public DbSet<vwPopularThisWeekModel> vwPopularThisWeek { get; set; }
        public DbSet<TopTenListModel> TopTenList { get; set; }
        public DbSet<EmbeddedMusicModel> EmbeddedMusic { get; set; } 



        //Subscriptions Model
        public DbSet<SubscribersModel> Subscribers { get; set; }
        public DbSet<CategoriesSubscriptionModel> CategoriesSubscription { get; set; }
        public DbSet<TagsSubscriptionModel> TagsSubscription { get; set; }
        public DbSet<AuthorsSubscriptionModel> AuthorsSubscription { get; set; }



        //Initialize DbContextOptions within the context, used in AppHelper file
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           //optionsBuilder.UseSqlServer("Server=sql5053.site4now.net;Database=DB_A68691_akassama;uid=DB_A68691_akassama_admin;pwd=Mg8JXn_TPFpJGdF;MultipleActiveResultSets=True");
           optionsBuilder.UseSqlServer("Server=DESKTOP-O81UVC0\\SQLEXPRESS;Database=NET_NEWS_CORE;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

    }
}
