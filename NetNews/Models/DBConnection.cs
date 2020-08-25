using Microsoft.EntityFrameworkCore;
using NetNews.Models.AccessDataModels;
using NetNews.Models.AccountsDataModel;
using NetNews.Models.AppDataModels;
using NetNews.Models.PostsDataModel;
using NetNews.Models.SubscriptionsDataModel;
using NetNews.Models.ThemeDataModels;
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
        public DbSet<LoginInfoModel> LoginInfo { get; set; }
        public DbSet<LoginViewModel> LoginView { get; set; }
        public DbSet<SiteDataLookupModel> SiteDataLookup { get; set; }
        public DbSet<ActivityLogsModel> ActivityLogs { get; set; }
        public DbSet<ContactMessagesModel> ContactMessages { get; set; }
        public DbSet<MessageViewsModel> MessageViews { get; set; }


        //Access Data Models
        public DbSet<RolesModel> Roles { get; set; }
        public DbSet<PermissionsModel> Permissions { get; set; }
        public DbSet<AccountToRoleModel> AccountToRole { get; set; }
        public DbSet<RoleToPermissionModel> RoleToPermission { get; set; }


        //Posts Models
        public DbSet<PostsModel> Posts { get; set; }
        public DbSet<CategoriesModel> Categories { get; set; }
        public DbSet<TagsModel> Tags { get; set; }
        public DbSet<GalleriesModel> Galleries { get; set; }
        public DbSet<GalleryImagesModel> GalleryImages { get; set; }
        public DbSet<VideosModel> VideosModel { get; set; }
        public DbSet<VideoUploadsModel> VideoUploads { get; set; }
        public DbSet<PostApprovalsModel> PostApprovals { get; set; }
        public DbSet<PostViewsModel> PostViews { get; set; }
        public DbSet<RevisionHistoryModel> RevisionHistory { get; set; }



        //Subscriptions Model
        public DbSet<SubscribersModel> Subscribers { get; set; }
        public DbSet<CategoriesSubscriptionModel> CategoriesSubscription { get; set; }
        public DbSet<TagsSubscriptionModel> TagsSubscription { get; set; }
        public DbSet<AuthorsSubscriptionModel> AuthorsSubscription { get; set; }


        //Themes Model
        public DbSet<ThemesModel> Themes { get; set; }
        public DbSet<ThemeSettingsModel> ThemeSettings { get; set; }



        //Initialize DbContextOptions within the context, used in AppHelper file
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=DESKTOP-O81UVC0\\SQLEXPRESS;database=IMPACT_GAMBIA;trusted_connection=true;");
        }

    }
}
