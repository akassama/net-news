USE [master]
GO
/****** Object:  Database [NET_NEWS_CORE]    Script Date: 2/10/2021 7:03:42 PM ******/
CREATE DATABASE [NET_NEWS_CORE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IMPACT_GAMBIA', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IMPACT_GAMBIA.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IMPACT_GAMBIA_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\IMPACT_GAMBIA_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [NET_NEWS_CORE] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NET_NEWS_CORE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NET_NEWS_CORE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET ARITHABORT OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NET_NEWS_CORE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NET_NEWS_CORE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [NET_NEWS_CORE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NET_NEWS_CORE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [NET_NEWS_CORE] SET  MULTI_USER 
GO
ALTER DATABASE [NET_NEWS_CORE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NET_NEWS_CORE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NET_NEWS_CORE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NET_NEWS_CORE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NET_NEWS_CORE] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NET_NEWS_CORE] SET QUERY_STORE = OFF
GO
USE [NET_NEWS_CORE]
GO
/****** Object:  Table [dbo].[PostApprovals]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostApprovals](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NOT NULL,
	[PostType] [varchar](250) NULL,
	[ApprovalState] [int] NOT NULL,
	[ApprovedBy] [varchar](250) NULL,
	[DateApproved] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Posts]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Posts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NULL,
	[PostType] [varchar](50) NULL,
	[PostPermalink] [varchar](250) NULL,
	[PostAuthor] [varchar](250) NOT NULL,
	[PostCategory] [varchar](250) NOT NULL,
	[PostSubCategory] [varchar](250) NULL,
	[PostTitle] [varchar](250) NOT NULL,
	[PostExtract] [text] NULL,
	[PostImage] [varchar](250) NULL,
	[ImageCaption] [varchar](250) NULL,
	[IsBreakingNews] [int] NULL,
	[FeaturedPost] [int] NULL,
	[PostContent] [text] NULL,
	[PostVideoType] [varchar](250) NULL,
	[PostVideoLink] [varchar](250) NULL,
	[PostAudioType] [varchar](250) NULL,
	[PostAudioLink] [varchar](250) NULL,
	[PostTags] [varchar](250) NULL,
	[PostEditor] [varchar](250) NOT NULL,
	[MetaTitle] [varchar](250) NULL,
	[MetaDescription] [varchar](250) NULL,
	[MetaKeywords] [varchar](250) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPosts]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPosts]
AS
SELECT        dbo.Posts.ID, dbo.Posts.PostID, dbo.Posts.PostPermalink, dbo.Posts.PostAuthor, dbo.Posts.PostCategory, dbo.Posts.PostSubCategory, dbo.Posts.PostTitle, dbo.Posts.PostExtract, dbo.Posts.PostImage, 
                         dbo.Posts.ImageCaption, dbo.Posts.IsBreakingNews, dbo.Posts.FeaturedPost, dbo.Posts.PostContent, dbo.Posts.PostVideoType, dbo.Posts.PostVideoLink, dbo.Posts.PostAudioType, dbo.Posts.PostAudioLink, 
                         dbo.Posts.PostTags, dbo.Posts.PostEditor, dbo.Posts.MetaTitle, dbo.Posts.MetaDescription, dbo.Posts.MetaKeywords, dbo.Posts.UpdatedBy, dbo.Posts.UpdateDate, dbo.Posts.DateAdded, dbo.PostApprovals.ID AS ApprovalsID,
                          dbo.PostApprovals.PostID AS ApprovalsPostID, dbo.PostApprovals.PostType, dbo.PostApprovals.ApprovalState, dbo.PostApprovals.ApprovedBy, dbo.PostApprovals.DateApproved, 
                         dbo.PostApprovals.DateAdded AS ApprovalsDateAdded
FROM            dbo.Posts INNER JOIN
                         dbo.PostApprovals ON dbo.Posts.PostID = dbo.PostApprovals.PostID
GO
/****** Object:  View [dbo].[vwPostsApproved]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPostsApproved]
AS
SELECT        ID, PostID, PostType, PostPermalink, PostAuthor, PostCategory, PostSubCategory, PostTitle, PostExtract, PostImage, ImageCaption, IsBreakingNews, FeaturedPost, PostContent, PostVideoType, PostVideoLink, PostAudioType, 
                         PostAudioLink, PostTags, PostEditor, MetaTitle, MetaDescription, MetaKeywords, UpdatedBy, UpdateDate, DateAdded, ApprovalsID, ApprovalsPostID, ApprovalState, ApprovedBy, DateApproved, ApprovalsDateAdded
FROM            dbo.vwPosts
WHERE        (ApprovalState = '1')
GO
/****** Object:  Table [dbo].[PostViews]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostViews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NOT NULL,
	[PostAuthor] [varchar](250) NULL,
	[PostType] [varchar](250) NOT NULL,
	[IpAddress] [varchar](250) NULL,
	[Country] [varchar](250) NULL,
	[Browser] [varchar](250) NULL,
	[Device] [varchar](250) NULL,
	[VisitDate] [datetime] NULL,
	[OtherDetails] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPopularThisWeek]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPopularThisWeek]
AS
SELECT        TOP (50) PostID, COUNT(PostID) AS ValueOccurrence
FROM            dbo.PostViews
WHERE        (VisitDate >= GETDATE() - 7) AND (VisitDate < GETDATE()) AND (PostID IN
                             (SELECT        PostID
                               FROM            dbo.Posts))
GROUP BY PostID
ORDER BY 'ValueOccurrence' DESC
GO
/****** Object:  Table [dbo].[PostReviews]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostReviews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NOT NULL,
	[ReviewerID] [varchar](250) NOT NULL,
	[ReviewComment] [text] NOT NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPostReviews]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwPostReviews]
AS
SELECT        dbo.vwPosts.ID, dbo.vwPosts.PostID, dbo.vwPosts.PostAuthor, dbo.vwPosts.PostCategory, dbo.vwPosts.PostTitle, dbo.vwPosts.ApprovalState, dbo.vwPosts.DateAdded, dbo.PostReviews.PostID AS ReviewsPostID, 
                         dbo.PostReviews.ReviewerID, dbo.PostReviews.ReviewComment
FROM            dbo.vwPosts INNER JOIN
                         dbo.PostReviews ON dbo.vwPosts.PostID = dbo.PostReviews.PostID
GO
/****** Object:  Table [dbo].[AccountDetails]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](250) NOT NULL,
	[Country] [varchar](250) NULL,
	[CountryCode] [int] NULL,
	[PhoneNumber] [varchar](250) NULL,
	[PhoneNumberVerification] [int] NULL,
	[Biography] [text] NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [varchar](50) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Accounts]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](250) NOT NULL,
	[FirstName] [varchar](250) NULL,
	[LastName] [varchar](250) NULL,
	[Email] [varchar](250) NOT NULL,
	[Password] [varchar](500) NOT NULL,
	[ProfilePicture] [varchar](250) NULL,
	[Active] [int] NOT NULL,
	[Oauth] [int] NULL,
	[EmailVerification] [int] NULL,
	[DirectoryName] [varchar](250) NOT NULL,
	[RememberToken] [varchar](250) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountToPermission]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountToPermission](
	[AccountID] [varchar](250) NOT NULL,
	[PermissionID] [int] NOT NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ActivityLogs]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActivityLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ActivityUser] [varchar](250) NULL,
	[ActionBy] [varchar](250) NULL,
	[LogType] [varchar](250) NULL,
	[Action] [text] NOT NULL,
	[ActivityDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Adverts]    Script Date: 2/10/2021 7:03:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adverts](
	[AdvertID] [varchar](250) NOT NULL,
	[AdvertTitle] [varchar](250) NULL,
	[AdvertPermalink] [varchar](250) NULL,
	[AdvertImage] [varchar](250) NOT NULL,
	[AdvertText] [text] NOT NULL,
	[PostBy] [varchar](250) NULL,
	[ExpiryDate] [datetime] NULL,
	[DateAdded] [datetime] NULL,
 CONSTRAINT [PK__Adverts__4FE88F249E4C80CD] PRIMARY KEY CLUSTERED 
(
	[AdvertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AuthorsSubscription]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AuthorsSubscription](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [varchar](250) NOT NULL,
	[AuthorID] [varchar](250) NOT NULL,
	[DateSubscribed] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryID] [varchar](250) NOT NULL,
	[CategoryName] [varchar](250) NULL,
	[ShortCategoryName] [varchar](100) NULL,
	[CategoryParent] [varchar](250) NULL,
	[CategoryDescription] [text] NULL,
	[CategoryIcon] [varchar](250) NULL,
	[CategoryOrder] [int] NULL,
	[IsPublished] [int] NULL,
	[IsHeader] [int] NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CategoriesSubscription]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriesSubscription](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [varchar](250) NOT NULL,
	[CategoryID] [varchar](250) NOT NULL,
	[DateSubscribed] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactMessages]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactMessages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NULL,
	[Email] [varchar](250) NULL,
	[Phone] [varchar](250) NULL,
	[Subject] [varchar](250) NULL,
	[Message] [text] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Country]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Country](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ISO] [char](2) NOT NULL,
	[Name] [varchar](80) NOT NULL,
	[NiceName] [varchar](80) NOT NULL,
	[ISO3] [char](3) NULL,
	[NumCode] [smallint] NULL,
	[PhoneCode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmbeddedMusic]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmbeddedMusic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EmbedID] [varchar](250) NOT NULL,
	[EmbedTitle] [varchar](250) NULL,
	[EmbedType] [varchar](250) NULL,
	[EmbedCode] [text] NOT NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GalleryImages]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryImages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NOT NULL,
	[ImageLink] [varchar](250) NOT NULL,
	[ImageCaption] [varchar](500) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[JobID] [varchar](250) NOT NULL,
	[JobTitle] [varchar](250) NOT NULL,
	[JobAdvertPermalink] [varchar](250) NOT NULL,
	[Company] [varchar](250) NOT NULL,
	[Location] [varchar](250) NOT NULL,
	[ApplicationLink] [varchar](500) NULL,
	[JobPost] [text] NOT NULL,
	[PostBy] [varchar](250) NULL,
	[ExpiryDate] [datetime] NULL,
	[DateAdded] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MessageViews]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MessageViews](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MessageID] [varchar](250) NOT NULL,
	[AccountID] [varchar](250) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NewsApi]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NewsApi](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NULL,
	[Source] [varchar](50) NULL,
	[Category] [varchar](50) NULL,
	[Author] [varchar](250) NULL,
	[Title] [varchar](250) NULL,
	[Description] [varchar](250) NULL,
	[Url] [varchar](250) NULL,
	[UrlToImage] [varchar](250) NULL,
	[PublishedAt] [varchar](250) NULL,
	[Content] [text] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PasswordForgot]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PasswordForgot](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ResetID] [varchar](500) NOT NULL,
	[AccountID] [varchar](250) NULL,
	[ResetDate] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[PermissionID] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [varchar](250) NOT NULL,
	[PermissionDescription] [varchar](500) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteDataLookup]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteDataLookup](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UinqueKey] [varchar](250) NOT NULL,
	[DataType] [varchar](50) NULL,
	[DataOptions] [varchar](250) NULL,
	[DataGroup] [varchar](250) NULL,
	[Value] [text] NOT NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiteStats]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiteStats](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[StatType] [varchar](250) NOT NULL,
	[ActionValue] [varchar](500) NOT NULL,
	[IpAddress] [varchar](250) NULL,
	[Country] [varchar](250) NULL,
	[Browser] [varchar](250) NULL,
	[Device] [varchar](250) NULL,
	[ActionDate] [datetime] NULL,
	[OtherDetails] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SocialMediaDetails]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SocialMediaDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [varchar](250) NOT NULL,
	[WebsiteLink] [varchar](250) NULL,
	[FacebookLink] [varchar](250) NULL,
	[TwitterLink] [varchar](250) NULL,
	[InstagramLink] [varchar](250) NULL,
	[LinkedInLink] [varchar](250) NULL,
	[SkypeName] [varchar](250) NULL,
	[WhatsAppNumber] [varchar](250) NULL,
	[TelegramAppNumber] [varchar](50) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [varchar](250) NOT NULL,
	[FullName] [varchar](250) NULL,
	[SubscriberEmail] [varchar](250) NOT NULL,
	[Password] [varchar](500) NULL,
	[EmailVerification] [int] NULL,
	[ProfilePicture] [varchar](250) NULL,
	[DateSubscribed] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tags]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tags](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TagID] [varchar](250) NOT NULL,
	[TagName] [varchar](250) NULL,
	[ShortTagName] [varchar](100) NULL,
	[TagDescription] [varchar](500) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TagsSubscription]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TagsSubscription](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubscriberID] [varchar](250) NOT NULL,
	[TagID] [varchar](250) NOT NULL,
	[DateSubscribed] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TopTenList]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TopTenList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ListID] [varchar](250) NOT NULL,
	[ListType] [varchar](250) NOT NULL,
	[ListTitle] [varchar](250) NULL,
	[ListOrder] [int] NOT NULL,
	[ListLink] [varchar](250) NOT NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VideoUploads]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VideoUploads](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PostID] [varchar](250) NOT NULL,
	[VideoLink] [varchar](250) NOT NULL,
	[VideoCaption] [varchar](500) NULL,
	[UpdatedBy] [varchar](250) NULL,
	[UpdateDate] [datetime] NULL,
	[DateAdded] [datetime] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[VisitLogs]    Script Date: 2/10/2021 7:03:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[VisitLogs](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [varchar](250) NOT NULL,
	[PageTitle] [varchar](250) NULL,
	[IpAddress] [varchar](250) NULL,
	[Country] [varchar](250) NULL,
	[Browser] [varchar](250) NULL,
	[Device] [varchar](250) NULL,
	[VisitTime] [varchar](250) NULL,
	[VisitDate] [datetime] NULL,
	[OtherDetails] [varchar](500) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[AccountDetails] ON 

INSERT [dbo].[AccountDetails] ([ID], [AccountID], [Country], [CountryCode], [PhoneNumber], [PhoneNumberVerification], [Biography], [DateOfBirth], [Gender], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', N'78', 7, N'9961219106', 0, N'<p>Freelance journalist</p>', CAST(N'1991-10-19' AS Date), N'Male', NULL, NULL, CAST(N'2020-11-13T04:06:37.287' AS DateTime))
SET IDENTITY_INSERT [dbo].[AccountDetails] OFF
SET IDENTITY_INSERT [dbo].[Accounts] ON 

INSERT [dbo].[Accounts] ([ID], [AccountID], [FirstName], [LastName], [Email], [Password], [ProfilePicture], [Active], [Oauth], [EmailVerification], [DirectoryName], [RememberToken], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', N'Admin', N'User', N'admin@netnews.com', N'$2b$10$UilYzXvjwfpcTRWROMm85uRWaLyh4N/MlNEw5aZs/S2TH2jrWLp5W', N'9rNaUDPp-face.JPG', 1, 0, 0, N'abliekassamanz6focpb', NULL, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T04:06:36.600' AS DateTime), CAST(N'2020-11-13T04:06:36.600' AS DateTime))
SET IDENTITY_INSERT [dbo].[Accounts] OFF
INSERT [dbo].[AccountToPermission] ([AccountID], [PermissionID], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', 1, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T04:11:37.843' AS DateTime), CAST(N'2020-11-13T04:11:37.843' AS DateTime))
INSERT [dbo].[AccountToPermission] ([AccountID], [PermissionID], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', 2, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T04:11:37.843' AS DateTime), CAST(N'2020-11-13T04:11:37.843' AS DateTime))
INSERT [dbo].[AccountToPermission] ([AccountID], [PermissionID], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', 3, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T04:11:37.843' AS DateTime), CAST(N'2020-11-13T04:11:37.843' AS DateTime))
SET IDENTITY_INSERT [dbo].[ActivityLogs] ON 

INSERT [dbo].[ActivityLogs] ([ID], [ActivityUser], [ActionBy], [LogType], [Action], [ActivityDate]) VALUES (1, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', N'UserLogin', N'User ''Abdoulie Kassama'' logged in.', CAST(N'2021-01-13T11:37:06.457' AS DateTime))
SET IDENTITY_INSERT [dbo].[ActivityLogs] OFF
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'68e28b63-16a4-4755-ac15-787d7a89523d', N'News', N'News', NULL, N'News category', NULL, 1, 1, 1, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'9f5f27de-9537-4d0b-a2ff-f5fa96818cad', N'Politics', N'Politics', NULL, N'Politics category', NULL, 1, 1, 1, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (3, N'8edaa000-71d5-419f-a298-8c57779409b6', N'International', N'International', NULL, N'International category', NULL, 1, 1, 1, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (4, N'c18246ef-7e95-477e-90c2-c31da9d762f3', N'Business', N'Business', NULL, N'Business category', NULL, 1, 1, 1, N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (5, N'bf887d83-69cc-4eea-94b0-15fd91b134c6', N'The Gambia', N'TheGambia', N'', N'The Gambia', N'', 5, 1, 1, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T06:16:57.280' AS DateTime), CAST(N'2020-11-13T06:16:57.097' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (6, N'5b31c46d-050b-4ae5-873b-dc33c0d06636', N'Africa', N'Africa', N'', N'', N'', 7, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T07:57:01.607' AS DateTime), CAST(N'2020-11-13T07:57:01.570' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (7, N'c0c696cb-35c7-4ab0-b067-5239b0683f29', N'Health', N'Health', N'', N'', N'', 8, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T08:11:33.410' AS DateTime), CAST(N'2020-11-13T08:11:33.393' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (10, N'18550719-acb0-430b-86d8-06a837906087', N'Celebrity News', N'CelebrityNews', N'', N'Celebrity News', N'', 10, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:02:23.090' AS DateTime), CAST(N'2020-11-16T23:02:23.047' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (11, N'852afd1a-6a27-4350-b966-b7e11cf37c89', N'Music', N'Music', N'', N'Music Category', N'', 11, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:03:12.363' AS DateTime), CAST(N'2020-11-16T23:03:12.350' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (12, N'2cbc8abe-7de0-4e50-8c93-9011e18f7dda', N'Category', N'Category', N'', N'Category', N'', 13, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T10:02:56.650' AS DateTime), CAST(N'2020-11-16T23:42:12.650' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (13, N'2958782e-67e6-466e-b9b6-c1832862673a', N'Entertainment', N'Entertainment', N'', N'Entertainment', N'', 12, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T10:02:56.563' AS DateTime), CAST(N'2020-11-17T10:02:56.510' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (8, N'4c133807-6962-4399-a058-61ca74a9a8ed', N'US Election', N'USElection', N'', N'', N'', 9, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T08:16:06.180' AS DateTime), CAST(N'2020-11-13T08:16:06.173' AS DateTime))
INSERT [dbo].[Categories] ([ID], [CategoryID], [CategoryName], [ShortCategoryName], [CategoryParent], [CategoryDescription], [CategoryIcon], [CategoryOrder], [IsPublished], [IsHeader], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (9, N'0b95b80b-0c22-4ebc-8dfa-8c9493edad57', N'TRRC', N'TRRC', N'', N'', N'', 14, 1, 0, N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-19T00:46:42.747' AS DateTime), CAST(N'2020-11-13T08:16:57.803' AS DateTime))
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Country] ON 

INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (1, N'AF', N'AFGHANISTAN', N'Afghanistan', N'AFG', 4, 93)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (2, N'AL', N'ALBANIA', N'Albania', N'ALB', 8, 355)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (3, N'DZ', N'ALGERIA', N'Algeria', N'DZA', 12, 213)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (4, N'AS', N'AMERICAN SAMOA', N'American Samoa', N'ASM', 16, 1684)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (5, N'AD', N'ANDORRA', N'Andorra', N'AND', 20, 376)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (6, N'AO', N'ANGOLA', N'Angola', N'AGO', 24, 244)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (7, N'AI', N'ANGUILLA', N'Anguilla', N'AIA', 660, 1264)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (8, N'AQ', N'ANTARCTICA', N'Antarctica', NULL, NULL, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (9, N'AG', N'ANTIGUA AND BARBUDA', N'Antigua and Barbuda', N'ATG', 28, 1268)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (10, N'AR', N'ARGENTINA', N'Argentina', N'ARG', 32, 54)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (11, N'AM', N'ARMENIA', N'Armenia', N'ARM', 51, 374)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (12, N'AW', N'ARUBA', N'Aruba', N'ABW', 533, 297)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (13, N'AU', N'AUSTRALIA', N'Australia', N'AUS', 36, 61)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (14, N'AT', N'AUSTRIA', N'Austria', N'AUT', 40, 43)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (15, N'AZ', N'AZERBAIJAN', N'Azerbaijan', N'AZE', 31, 994)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (16, N'BS', N'BAHAMAS', N'Bahamas', N'BHS', 44, 1242)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (17, N'BH', N'BAHRAIN', N'Bahrain', N'BHR', 48, 973)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (18, N'BD', N'BANGLADESH', N'Bangladesh', N'BGD', 50, 880)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (19, N'BB', N'BARBADOS', N'Barbados', N'BRB', 52, 1246)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (20, N'BY', N'BELARUS', N'Belarus', N'BLR', 112, 375)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (21, N'BE', N'BELGIUM', N'Belgium', N'BEL', 56, 32)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (22, N'BZ', N'BELIZE', N'Belize', N'BLZ', 84, 501)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (23, N'BJ', N'BENIN', N'Benin', N'BEN', 204, 229)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (24, N'BM', N'BERMUDA', N'Bermuda', N'BMU', 60, 1441)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (25, N'BT', N'BHUTAN', N'Bhutan', N'BTN', 64, 975)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (26, N'BO', N'BOLIVIA', N'Bolivia', N'BOL', 68, 591)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (27, N'BA', N'BOSNIA AND HERZEGOVINA', N'Bosnia and Herzegovina', N'BIH', 70, 387)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (28, N'BW', N'BOTSWANA', N'Botswana', N'BWA', 72, 267)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (29, N'BV', N'BOUVET ISLAND', N'Bouvet Island', NULL, NULL, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (30, N'BR', N'BRAZIL', N'Brazil', N'BRA', 76, 55)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (31, N'IO', N'BRITISH INDIAN OCEAN TERRITORY', N'British Indian Ocean Territory', NULL, NULL, 246)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (32, N'BN', N'BRUNEI DARUSSALAM', N'Brunei Darussalam', N'BRN', 96, 673)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (33, N'BG', N'BULGARIA', N'Bulgaria', N'BGR', 100, 359)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (34, N'BF', N'BURKINA FASO', N'Burkina Faso', N'BFA', 854, 226)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (35, N'BI', N'BURUNDI', N'Burundi', N'BDI', 108, 257)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (36, N'KH', N'CAMBODIA', N'Cambodia', N'KHM', 116, 855)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (37, N'CM', N'CAMEROON', N'Cameroon', N'CMR', 120, 237)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (38, N'CA', N'CANADA', N'Canada', N'CAN', 124, 1)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (39, N'CV', N'CAPE VERDE', N'Cape Verde', N'CPV', 132, 238)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (40, N'KY', N'CAYMAN ISLANDS', N'Cayman Islands', N'CYM', 136, 1345)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (41, N'CF', N'CENTRAL AFRICAN REPUBLIC', N'Central African Republic', N'CAF', 140, 236)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (42, N'TD', N'CHAD', N'Chad', N'TCD', 148, 235)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (43, N'CL', N'CHILE', N'Chile', N'CHL', 152, 56)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (44, N'CN', N'CHINA', N'China', N'CHN', 156, 86)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (45, N'CX', N'CHRISTMAS ISLAND', N'Christmas Island', NULL, NULL, 61)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (46, N'CC', N'COCOS (KEELING) ISLANDS', N'Cocos (Keeling) Islands', NULL, NULL, 672)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (47, N'CO', N'COLOMBIA', N'Colombia', N'COL', 170, 57)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (48, N'KM', N'COMOROS', N'Comoros', N'COM', 174, 269)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (49, N'CG', N'CONGO', N'Congo', N'COG', 178, 242)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (50, N'CD', N'CONGO, THE DEMOCRATIC REPUBLIC OF THE', N'Congo, the Democratic Republic of the', N'COD', 180, 242)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (51, N'CK', N'COOK ISLANDS', N'Cook Islands', N'COK', 184, 682)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (52, N'CR', N'COSTA RICA', N'Costa Rica', N'CRI', 188, 506)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (53, N'CI', N'COTE D''IVOIRE', N'Cote D''Ivoire', N'CIV', 384, 225)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (54, N'HR', N'CROATIA', N'Croatia', N'HRV', 191, 385)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (55, N'CU', N'CUBA', N'Cuba', N'CUB', 192, 53)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (56, N'CY', N'CYPRUS', N'Cyprus', N'CYP', 196, 357)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (57, N'CZ', N'CZECH REPUBLIC', N'Czech Republic', N'CZE', 203, 420)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (58, N'DK', N'DENMARK', N'Denmark', N'DNK', 208, 45)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (59, N'DJ', N'DJIBOUTI', N'Djibouti', N'DJI', 262, 253)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (60, N'DM', N'DOMINICA', N'Dominica', N'DMA', 212, 1767)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (61, N'DO', N'DOMINICAN REPUBLIC', N'Dominican Republic', N'DOM', 214, 1809)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (62, N'EC', N'ECUADOR', N'Ecuador', N'ECU', 218, 593)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (63, N'EG', N'EGYPT', N'Egypt', N'EGY', 818, 20)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (64, N'SV', N'EL SALVADOR', N'El Salvador', N'SLV', 222, 503)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (65, N'GQ', N'EQUATORIAL GUINEA', N'Equatorial Guinea', N'GNQ', 226, 240)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (66, N'ER', N'ERITREA', N'Eritrea', N'ERI', 232, 291)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (67, N'EE', N'ESTONIA', N'Estonia', N'EST', 233, 372)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (68, N'ET', N'ETHIOPIA', N'Ethiopia', N'ETH', 231, 251)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (69, N'FK', N'FALKLAND ISLANDS (MALVINAS)', N'Falkland Islands (Malvinas)', N'FLK', 238, 500)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (70, N'FO', N'FAROE ISLANDS', N'Faroe Islands', N'FRO', 234, 298)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (71, N'FJ', N'FIJI', N'Fiji', N'FJI', 242, 679)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (72, N'FI', N'FINLAND', N'Finland', N'FIN', 246, 358)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (73, N'FR', N'FRANCE', N'France', N'FRA', 250, 33)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (74, N'GF', N'FRENCH GUIANA', N'French Guiana', N'GUF', 254, 594)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (75, N'PF', N'FRENCH POLYNESIA', N'French Polynesia', N'PYF', 258, 689)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (76, N'TF', N'FRENCH SOUTHERN TERRITORIES', N'French Southern Territories', NULL, NULL, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (77, N'GA', N'GABON', N'Gabon', N'GAB', 266, 241)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (78, N'GM', N'GAMBIA', N'Gambia', N'GMB', 270, 220)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (79, N'GE', N'GEORGIA', N'Georgia', N'GEO', 268, 995)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (80, N'DE', N'GERMANY', N'Germany', N'DEU', 276, 49)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (81, N'GH', N'GHANA', N'Ghana', N'GHA', 288, 233)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (82, N'GI', N'GIBRALTAR', N'Gibraltar', N'GIB', 292, 350)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (83, N'GR', N'GREECE', N'Greece', N'GRC', 300, 30)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (84, N'GL', N'GREENLAND', N'Greenland', N'GRL', 304, 299)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (85, N'GD', N'GRENADA', N'Grenada', N'GRD', 308, 1473)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (86, N'GP', N'GUADELOUPE', N'Guadeloupe', N'GLP', 312, 590)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (87, N'GU', N'GUAM', N'Guam', N'GUM', 316, 1671)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (88, N'GT', N'GUATEMALA', N'Guatemala', N'GTM', 320, 502)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (89, N'GN', N'GUINEA', N'Guinea', N'GIN', 324, 224)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (90, N'GW', N'GUINEA-BISSAU', N'Guinea-Bissau', N'GNB', 624, 245)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (91, N'GY', N'GUYANA', N'Guyana', N'GUY', 328, 592)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (92, N'HT', N'HAITI', N'Haiti', N'HTI', 332, 509)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (93, N'HM', N'HEARD ISLAND AND MCDONALD ISLANDS', N'Heard Island and Mcdonald Islands', NULL, NULL, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (94, N'VA', N'HOLY SEE (VATICAN CITY STATE)', N'Holy See (Vatican City State)', N'VAT', 336, 39)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (95, N'HN', N'HONDURAS', N'Honduras', N'HND', 340, 504)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (96, N'HK', N'HONG KONG', N'Hong Kong', N'HKG', 344, 852)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (97, N'HU', N'HUNGARY', N'Hungary', N'HUN', 348, 36)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (98, N'IS', N'ICELAND', N'Iceland', N'ISL', 352, 354)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (99, N'IN', N'INDIA', N'India', N'IND', 356, 91)
GO
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (100, N'ID', N'INDONESIA', N'Indonesia', N'IDN', 360, 62)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (101, N'IR', N'IRAN, ISLAMIC REPUBLIC OF', N'Iran, Islamic Republic of', N'IRN', 364, 98)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (102, N'IQ', N'IRAQ', N'Iraq', N'IRQ', 368, 964)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (103, N'IE', N'IRELAND', N'Ireland', N'IRL', 372, 353)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (104, N'IL', N'ISRAEL', N'Israel', N'ISR', 376, 972)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (105, N'IT', N'ITALY', N'Italy', N'ITA', 380, 39)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (106, N'JM', N'JAMAICA', N'Jamaica', N'JAM', 388, 1876)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (107, N'JP', N'JAPAN', N'Japan', N'JPN', 392, 81)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (108, N'JO', N'JORDAN', N'Jordan', N'JOR', 400, 962)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (109, N'KZ', N'KAZAKHSTAN', N'Kazakhstan', N'KAZ', 398, 7)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (110, N'KE', N'KENYA', N'Kenya', N'KEN', 404, 254)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (111, N'KI', N'KIRIBATI', N'Kiribati', N'KIR', 296, 686)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (112, N'KP', N'KOREA, DEMOCRATIC PEOPLE''S REPUBLIC OF', N'Korea, Democratic People''s Republic of', N'PRK', 408, 850)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (113, N'KR', N'KOREA, REPUBLIC OF', N'Korea, Republic of', N'KOR', 410, 82)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (114, N'KW', N'KUWAIT', N'Kuwait', N'KWT', 414, 965)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (115, N'KG', N'KYRGYZSTAN', N'Kyrgyzstan', N'KGZ', 417, 996)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (116, N'LA', N'LAO PEOPLE''S DEMOCRATIC REPUBLIC', N'Lao People''s Democratic Republic', N'LAO', 418, 856)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (117, N'LV', N'LATVIA', N'Latvia', N'LVA', 428, 371)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (118, N'LB', N'LEBANON', N'Lebanon', N'LBN', 422, 961)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (119, N'LS', N'LESOTHO', N'Lesotho', N'LSO', 426, 266)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (120, N'LR', N'LIBERIA', N'Liberia', N'LBR', 430, 231)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (121, N'LY', N'LIBYAN ARAB JAMAHIRIYA', N'Libyan Arab Jamahiriya', N'LBY', 434, 218)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (122, N'LI', N'LIECHTENSTEIN', N'Liechtenstein', N'LIE', 438, 423)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (123, N'LT', N'LITHUANIA', N'Lithuania', N'LTU', 440, 370)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (124, N'LU', N'LUXEMBOURG', N'Luxembourg', N'LUX', 442, 352)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (125, N'MO', N'MACAO', N'Macao', N'MAC', 446, 853)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (126, N'MK', N'MACEDONIA, THE FORMER YUGOSLAV REPUBLIC OF', N'Macedonia, the Former Yugoslav Republic of', N'MKD', 807, 389)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (127, N'MG', N'MADAGASCAR', N'Madagascar', N'MDG', 450, 261)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (128, N'MW', N'MALAWI', N'Malawi', N'MWI', 454, 265)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (129, N'MY', N'MALAYSIA', N'Malaysia', N'MYS', 458, 60)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (130, N'MV', N'MALDIVES', N'Maldives', N'MDV', 462, 960)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (131, N'ML', N'MALI', N'Mali', N'MLI', 466, 223)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (132, N'MT', N'MALTA', N'Malta', N'MLT', 470, 356)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (133, N'MH', N'MARSHALL ISLANDS', N'Marshall Islands', N'MHL', 584, 692)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (134, N'MQ', N'MARTINIQUE', N'Martinique', N'MTQ', 474, 596)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (135, N'MR', N'MAURITANIA', N'Mauritania', N'MRT', 478, 222)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (136, N'MU', N'MAURITIUS', N'Mauritius', N'MUS', 480, 230)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (137, N'YT', N'MAYOTTE', N'Mayotte', NULL, NULL, 269)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (138, N'MX', N'MEXICO', N'Mexico', N'MEX', 484, 52)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (139, N'FM', N'MICRONESIA, FEDERATED STATES OF', N'Micronesia, Federated States of', N'FSM', 583, 691)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (140, N'MD', N'MOLDOVA, REPUBLIC OF', N'Moldova, Republic of', N'MDA', 498, 373)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (141, N'MC', N'MONACO', N'Monaco', N'MCO', 492, 377)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (142, N'MN', N'MONGOLIA', N'Mongolia', N'MNG', 496, 976)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (143, N'MS', N'MONTSERRAT', N'Montserrat', N'MSR', 500, 1664)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (144, N'MA', N'MOROCCO', N'Morocco', N'MAR', 504, 212)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (145, N'MZ', N'MOZAMBIQUE', N'Mozambique', N'MOZ', 508, 258)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (146, N'MM', N'MYANMAR', N'Myanmar', N'MMR', 104, 95)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (147, N'NA', N'NAMIBIA', N'Namibia', N'NAM', 516, 264)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (148, N'NR', N'NAURU', N'Nauru', N'NRU', 520, 674)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (149, N'NP', N'NEPAL', N'Nepal', N'NPL', 524, 977)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (150, N'NL', N'NETHERLANDS', N'Netherlands', N'NLD', 528, 31)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (151, N'AN', N'NETHERLANDS ANTILLES', N'Netherlands Antilles', N'ANT', 530, 599)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (152, N'NC', N'NEW CALEDONIA', N'New Caledonia', N'NCL', 540, 687)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (153, N'NZ', N'NEW ZEALAND', N'New Zealand', N'NZL', 554, 64)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (154, N'NI', N'NICARAGUA', N'Nicaragua', N'NIC', 558, 505)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (155, N'NE', N'NIGER', N'Niger', N'NER', 562, 227)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (156, N'NG', N'NIGERIA', N'Nigeria', N'NGA', 566, 234)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (157, N'NU', N'NIUE', N'Niue', N'NIU', 570, 683)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (158, N'NF', N'NORFOLK ISLAND', N'Norfolk Island', N'NFK', 574, 672)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (159, N'MP', N'NORTHERN MARIANA ISLANDS', N'Northern Mariana Islands', N'MNP', 580, 1670)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (160, N'NO', N'NORWAY', N'Norway', N'NOR', 578, 47)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (161, N'OM', N'OMAN', N'Oman', N'OMN', 512, 968)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (162, N'PK', N'PAKISTAN', N'Pakistan', N'PAK', 586, 92)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (163, N'PW', N'PALAU', N'Palau', N'PLW', 585, 680)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (164, N'PS', N'PALESTINIAN TERRITORY, OCCUPIED', N'Palestinian Territory, Occupied', NULL, NULL, 970)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (165, N'PA', N'PANAMA', N'Panama', N'PAN', 591, 507)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (166, N'PG', N'PAPUA NEW GUINEA', N'Papua New Guinea', N'PNG', 598, 675)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (167, N'PY', N'PARAGUAY', N'Paraguay', N'PRY', 600, 595)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (168, N'PE', N'PERU', N'Peru', N'PER', 604, 51)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (169, N'PH', N'PHILIPPINES', N'Philippines', N'PHL', 608, 63)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (170, N'PN', N'PITCAIRN', N'Pitcairn', N'PCN', 612, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (171, N'PL', N'POLAND', N'Poland', N'POL', 616, 48)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (172, N'PT', N'PORTUGAL', N'Portugal', N'PRT', 620, 351)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (173, N'PR', N'PUERTO RICO', N'Puerto Rico', N'PRI', 630, 1787)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (174, N'QA', N'QATAR', N'Qatar', N'QAT', 634, 974)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (175, N'RE', N'REUNION', N'Reunion', N'REU', 638, 262)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (176, N'RO', N'ROMANIA', N'Romania', N'ROM', 642, 40)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (177, N'RU', N'RUSSIAN FEDERATION', N'Russian Federation', N'RUS', 643, 70)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (178, N'RW', N'RWANDA', N'Rwanda', N'RWA', 646, 250)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (179, N'SH', N'SAINT HELENA', N'Saint Helena', N'SHN', 654, 290)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (180, N'KN', N'SAINT KITTS AND NEVIS', N'Saint Kitts and Nevis', N'KNA', 659, 1869)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (181, N'LC', N'SAINT LUCIA', N'Saint Lucia', N'LCA', 662, 1758)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (182, N'PM', N'SAINT PIERRE AND MIQUELON', N'Saint Pierre and Miquelon', N'SPM', 666, 508)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (183, N'VC', N'SAINT VINCENT AND THE GRENADINES', N'Saint Vincent and the Grenadines', N'VCT', 670, 1784)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (184, N'WS', N'SAMOA', N'Samoa', N'WSM', 882, 684)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (185, N'SM', N'SAN MARINO', N'San Marino', N'SMR', 674, 378)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (186, N'ST', N'SAO TOME AND PRINCIPE', N'Sao Tome and Principe', N'STP', 678, 239)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (187, N'SA', N'SAUDI ARABIA', N'Saudi Arabia', N'SAU', 682, 966)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (188, N'SN', N'SENEGAL', N'Senegal', N'SEN', 686, 221)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (189, N'CS', N'SERBIA AND MONTENEGRO', N'Serbia and Montenegro', NULL, NULL, 381)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (190, N'SC', N'SEYCHELLES', N'Seychelles', N'SYC', 690, 248)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (191, N'SL', N'SIERRA LEONE', N'Sierra Leone', N'SLE', 694, 232)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (192, N'SG', N'SINGAPORE', N'Singapore', N'SGP', 702, 65)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (193, N'SK', N'SLOVAKIA', N'Slovakia', N'SVK', 703, 421)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (194, N'SI', N'SLOVENIA', N'Slovenia', N'SVN', 705, 386)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (195, N'SB', N'SOLOMON ISLANDS', N'Solomon Islands', N'SLB', 90, 677)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (196, N'SO', N'SOMALIA', N'Somalia', N'SOM', 706, 252)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (197, N'ZA', N'SOUTH AFRICA', N'South Africa', N'ZAF', 710, 27)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (198, N'GS', N'SOUTH GEORGIA AND THE SOUTH SANDWICH ISLANDS', N'South Georgia and the South Sandwich Islands', NULL, NULL, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (199, N'ES', N'SPAIN', N'Spain', N'ESP', 724, 34)
GO
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (200, N'LK', N'SRI LANKA', N'Sri Lanka', N'LKA', 144, 94)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (201, N'SD', N'SUDAN', N'Sudan', N'SDN', 736, 249)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (202, N'SR', N'SURINAME', N'Suriname', N'SUR', 740, 597)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (203, N'SJ', N'SVALBARD AND JAN MAYEN', N'Svalbard and Jan Mayen', N'SJM', 744, 47)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (204, N'SZ', N'SWAZILAND', N'Swaziland', N'SWZ', 748, 268)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (205, N'SE', N'SWEDEN', N'Sweden', N'SWE', 752, 46)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (206, N'CH', N'SWITZERLAND', N'Switzerland', N'CHE', 756, 41)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (207, N'SY', N'SYRIAN ARAB REPUBLIC', N'Syrian Arab Republic', N'SYR', 760, 963)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (208, N'TW', N'TAIWAN, PROVINCE OF CHINA', N'Taiwan, Province of China', N'TWN', 158, 886)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (209, N'TJ', N'TAJIKISTAN', N'Tajikistan', N'TJK', 762, 992)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (210, N'TZ', N'TANZANIA, UNITED REPUBLIC OF', N'Tanzania, United Republic of', N'TZA', 834, 255)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (211, N'TH', N'THAILAND', N'Thailand', N'THA', 764, 66)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (212, N'TL', N'TIMOR-LESTE', N'Timor-Leste', NULL, NULL, 670)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (213, N'TG', N'TOGO', N'Togo', N'TGO', 768, 228)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (214, N'TK', N'TOKELAU', N'Tokelau', N'TKL', 772, 690)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (215, N'TO', N'TONGA', N'Tonga', N'TON', 776, 676)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (216, N'TT', N'TRINIDAD AND TOBAGO', N'Trinidad and Tobago', N'TTO', 780, 1868)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (217, N'TN', N'TUNISIA', N'Tunisia', N'TUN', 788, 216)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (218, N'TR', N'TURKEY', N'Turkey', N'TUR', 792, 90)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (219, N'TM', N'TURKMENISTAN', N'Turkmenistan', N'TKM', 795, 7370)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (220, N'TC', N'TURKS AND CAICOS ISLANDS', N'Turks and Caicos Islands', N'TCA', 796, 1649)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (221, N'TV', N'TUVALU', N'Tuvalu', N'TUV', 798, 688)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (222, N'UG', N'UGANDA', N'Uganda', N'UGA', 800, 256)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (223, N'UA', N'UKRAINE', N'Ukraine', N'UKR', 804, 380)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (224, N'AE', N'UNITED ARAB EMIRATES', N'United Arab Emirates', N'ARE', 784, 971)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (225, N'GB', N'UNITED KINGDOM', N'United Kingdom', N'GBR', 826, 44)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (226, N'US', N'UNITED STATES', N'United States', N'USA', 840, 1)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (227, N'UM', N'UNITED STATES MINOR OUTLYING ISLANDS', N'United States Minor Outlying Islands', NULL, NULL, 1)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (228, N'UY', N'URUGUAY', N'Uruguay', N'URY', 858, 598)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (229, N'UZ', N'UZBEKISTAN', N'Uzbekistan', N'UZB', 860, 998)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (230, N'VU', N'VANUATU', N'Vanuatu', N'VUT', 548, 678)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (231, N'VE', N'VENEZUELA', N'Venezuela', N'VEN', 862, 58)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (232, N'VN', N'VIET NAM', N'Viet Nam', N'VNM', 704, 84)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (233, N'VG', N'VIRGIN ISLANDS, BRITISH', N'Virgin Islands, British', N'VGB', 92, 1284)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (234, N'VI', N'VIRGIN ISLANDS, U.S.', N'Virgin Islands, U.s.', N'VIR', 850, 1340)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (235, N'WF', N'WALLIS AND FUTUNA', N'Wallis and Futuna', N'WLF', 876, 681)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (236, N'EH', N'WESTERN SAHARA', N'Western Sahara', N'ESH', 732, 212)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (237, N'YE', N'YEMEN', N'Yemen', N'YEM', 887, 967)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (238, N'ZM', N'ZAMBIA', N'Zambia', N'ZMB', 894, 260)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (239, N'ZW', N'ZIMBABWE', N'Zimbabwe', N'ZWE', 716, 263)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (240, N'RS', N'SERBIA', N'Serbia', N'SRB', 688, 381)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (241, N'AP', N'ASIA PACIFIC REGION', N'Asia / Pacific Region', N'0  ', 0, 0)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (242, N'ME', N'MONTENEGRO', N'Montenegro', N'MNE', 499, 382)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (243, N'AX', N'ALAND ISLANDS', N'Aland Islands', N'ALA', 248, 358)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (244, N'BQ', N'BONAIRE, SINT EUSTATIUS AND SABA', N'Bonaire, Sint Eustatius and Saba', N'BES', 535, 599)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (245, N'CW', N'CURACAO', N'Curacao', N'CUW', 531, 599)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (246, N'GG', N'GUERNSEY', N'Guernsey', N'GGY', 831, 44)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (247, N'IM', N'ISLE OF MAN', N'Isle of Man', N'IMN', 833, 44)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (248, N'JE', N'JERSEY', N'Jersey', N'JEY', 832, 44)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (249, N'XK', N'KOSOVO', N'Kosovo', N'---', 0, 381)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (250, N'BL', N'SAINT BARTHELEMY', N'Saint Barthelemy', N'BLM', 652, 590)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (251, N'MF', N'SAINT MARTIN', N'Saint Martin', N'MAF', 663, 590)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (252, N'SX', N'SINT MAARTEN', N'Sint Maarten', N'SXM', 534, 1)
INSERT [dbo].[Country] ([ID], [ISO], [Name], [NiceName], [ISO3], [NumCode], [PhoneCode]) VALUES (253, N'SS', N'SOUTH SUDAN', N'South Sudan', N'SSD', 728, 211)
SET IDENTITY_INSERT [dbo].[Country] OFF
SET IDENTITY_INSERT [dbo].[EmbeddedMusic] ON 

INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'9d486212-408b-4228-ad41-d4d4ecc1e9ba', N'Jizzle - Finnaly', N'AppleMusicAlbum', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="450" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/finally/1531993573"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:49:23.370' AS DateTime), CAST(N'2020-11-17T07:49:23.180' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'8f30a19b-0529-49b0-8fee-78d70a182de0', N'ST da Gambian Dream - Gambiana', N'AppleMusicAlbum', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="450" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/gambiana/1490922740"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:51:54.500' AS DateTime), CAST(N'2020-11-17T07:51:54.477' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (3, N'397e1864-ff4e-4eb6-a4fd-3ea8dadca9e9', N'Hussain Dada - No Friends', N'AppleMusicSingle', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="150" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/no-friends/1521413877?i=1521414224"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:53:32.937' AS DateTime), CAST(N'2020-11-17T07:53:32.910' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (4, N'4554a04d-ab60-4d58-a985-68114a15039b', N'ST da Gambian Dream - Kodo Bay Kodo', N'AppleMusicSingle', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="150" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/kodo-bay-kodo/1490922740?i=1490922746"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:55:33.200' AS DateTime), CAST(N'2020-11-17T07:55:33.187' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (5, N'3114cf3a-0521-4eb1-89c0-7805b3235323', N'Jizzle - Incase', N'AppleMusicSingle', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="150" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/incase/1531993573?i=1531993584"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:56:12.637' AS DateTime), CAST(N'2020-11-17T07:56:12.613' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (6, N'9fe65c87-a306-4b52-8a9c-02c3912650fc', N'Nobles Gambia ft. Attack - Nobles Gambia', N'AppleMusicSingle', N'<iframe allow="autoplay *; encrypted-media *;" frameborder="0" height="150" style="width:100%;max-width:660px;overflow:hidden;background:transparent;" sandbox="allow-forms allow-popups allow-same-origin allow-scripts allow-storage-access-by-user-activation allow-top-navigation-by-user-activation" src="https://embed.music.apple.com/us/album/buzz-feat-attack/1527824779?i=1527824785"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:57:50.693' AS DateTime), CAST(N'2020-11-17T07:57:50.673' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (7, N'8ab73806-6d94-4803-b3d2-2fa52c9f371b', N'K Camp - Turn Up For A Check ft Yo Gotti (Prod by Sonny Digital)', N'Soundcloud', N'<iframe width="100%" height="300" scrolling="no" frameborder="no" allow="autoplay" src="https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/141914751&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true"></iframe><div style="font-size: 10px; color: #cccccc;line-break: anywhere;word-break: normal;overflow: hidden;white-space: nowrap;text-overflow: ellipsis; font-family: Interstate,Lucida Grande,Lucida Sans Unicode,Lucida Sans,Garuda,Verdana,Tahoma,sans-serif;font-weight: 100;"><a href="https://soundcloud.com/kcamp427" title="K Camp" target="_blank" style="color: #cccccc; text-decoration: none;">K Camp</a> · <a href="https://soundcloud.com/kcamp427/k-camp-turn-up-for-a-check-1" title="K Camp - Turn Up For A Check ft Yo Gotti (Prod by Sonny Digital)" target="_blank" style="color: #cccccc; text-decoration: none;">K Camp - Turn Up For A Check ft Yo Gotti (Prod by Sonny Digital)</a></div>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T07:59:55.577' AS DateTime), CAST(N'2020-11-17T07:59:55.513' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (8, N'43738539-80a7-4a28-be39-0b96bf535599', N'AJ Scupa -AFRO MIX', N'Soundcloud', N'<iframe width="100%" height="300" scrolling="no" frameborder="no" allow="autoplay" src="https://w.soundcloud.com/player/?url=https%3A//api.soundcloud.com/tracks/523388751&color=%23ff5500&auto_play=false&hide_related=false&show_comments=true&show_user=true&show_reposts=false&show_teaser=true&visual=true"></iframe><div style="font-size: 10px; color: #cccccc;line-break: anywhere;word-break: normal;overflow: hidden;white-space: nowrap;text-overflow: ellipsis; font-family: Interstate,Lucida Grande,Lucida Sans Unicode,Lucida Sans,Garuda,Verdana,Tahoma,sans-serif;font-weight: 100;"><a href="https://soundcloud.com/zjscupa" title="zjscupa" target="_blank" style="color: #cccccc; text-decoration: none;">zjscupa</a> · <a href="https://soundcloud.com/zjscupa/afro-mix-3" title="AFRO MIX" target="_blank" style="color: #cccccc; text-decoration: none;">AFRO MIX</a></div>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T08:00:31.517' AS DateTime), CAST(N'2020-11-17T08:00:31.507' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (9, N'54dafc38-8275-44a5-9ce9-6c0ae9d54ee0', N'ZJ SCUPA - AFRO MIX (BEST OF 2020) VOL1', N'MixCloud', N'<iframe width="100%" height="120" src="https://www.mixcloud.com/widget/iframe/?hide_cover=1&feed=%2FZJSCUPA%2Fzj-scupa-afro-mix-best-of-2019-vol2%2F" frameborder="0" ></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T08:01:41.257' AS DateTime), CAST(N'2020-11-17T08:01:41.237' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (10, N'5aecf175-5793-40aa-936f-702bf94a3e1b', N'ZJ SCUPA - AFRO MIX (BEST OF 2019) Vol1', N'MixCloud', N'<iframe width="100%" height="120" src="https://www.mixcloud.com/widget/iframe/?hide_cover=1&feed=%2FZJSCUPA%2Fzj-scupa-afro-mix-best-of-2019-vol1%2F" frameborder="0" ></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T08:02:27.250' AS DateTime), CAST(N'2020-11-17T08:02:27.233' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (11, N'73dd6054-8690-4714-a522-2797d27ec696', N'ZJ SCUPA AFRO MIX (BEST OF 2020) VOL1', N'Audiomack', N'<iframe src="https://audiomack.com/embed/song/zjscupa/afro-mix-best-of-2020-vol1?background=1" scrolling="no" width="100%" height="252" scrollbars="no" frameborder="0"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T08:05:36.817' AS DateTime), CAST(N'2020-11-17T08:05:36.797' AS DateTime))
INSERT [dbo].[EmbeddedMusic] ([ID], [EmbedID], [EmbedTitle], [EmbedType], [EmbedCode], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (12, N'3603d926-b48d-47af-8a2d-fa22e763b682', N'ZJ SCUPA AFRO MIX (BEST OF 2019)', N'Audiomack', N'<iframe src="https://audiomack.com/embed/song/zjscupa/afro-mix-best-of-2019-vol2?background=1" scrolling="no" width="100%" height="252" scrollbars="no" frameborder="0"></iframe>', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-17T08:06:07.377' AS DateTime), CAST(N'2020-11-17T08:06:07.367' AS DateTime))
SET IDENTITY_INSERT [dbo].[EmbeddedMusic] OFF
SET IDENTITY_INSERT [dbo].[NewsApi] ON 

INSERT [dbo].[NewsApi] ([ID], [PostID], [Source], [Category], [Author], [Title], [Description], [Url], [UrlToImage], [PublishedAt], [Content]) VALUES (1, N'06c743818989', N'CNN', N'Politics', N'Ali Zaslav, Clare Foran and Lauren Fox, CNN', N'Senate power-sharing agreement reached, Schumer announces, allowing Democrats to take control of committees - CNN', N'A deal has been reached in the Senate in principle on a power-sharing agreement that had precluded Democrats from taking control of the committees, Senate Majority Leader Chuck Schumer announced Wednesday morning.', N'https://www.cnn.com/2021/02/03/politics/senate-power-sharing-agreement-reached/index.html', N'https://cdn.cnn.com/cnnnext/dam/assets/210202094918-mcconnell-schumer-split-super-tease.jpg', N'2021-02-03', N'(CNN)A deal has been reached in the Senate in principle on a power-sharing agreement that had precluded Democrats from taking control of the committees, Senate Majority Leader Chuck Schumer announced… [+2517 chars]')
INSERT [dbo].[NewsApi] ([ID], [PostID], [Source], [Category], [Author], [Title], [Description], [Url], [UrlToImage], [PublishedAt], [Content]) VALUES (2, N'438106c78989', N'BBC News', N'News', N'BBC News', N'Their goal is to destroy everyone'': Uighur camp detainees allege systematic rape', N'In new testimony, former detainees of China''s detention camps describe systematic rape and torture.', N'https://www.bbc.com/news/world-asia-china-55794071', N'https://ichef.bbci.co.uk/news/1024/branded_news/18247/production/_116778889_tursunay_bbc_26jan21_11-2.jpg', N'2021-02-03', N'By Matthew Hill, David Campanale and Joel GunterBBC News\r\nWomen in China''s "re-education" camps for Uighurs have been systematically raped, sexually abused, and tortured, according to detailed new ac… [+20605 chars]')
SET IDENTITY_INSERT [dbo].[NewsApi] OFF
SET IDENTITY_INSERT [dbo].[PasswordForgot] ON 

INSERT [dbo].[PasswordForgot] ([ID], [ResetID], [AccountID], [ResetDate]) VALUES (1, N'67K7PK1BkKCENPeFpUsDrVuCvzmZe8zrMFjQYjWOx9PEEWZ7XQX2vjQlMHLZichmUU2HpNrF8dPsGq7ZaoBNk76mz8EQY6qUbmuueRRJiS4RTiefJw93bTCX', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2021-01-18T14:37:15.300' AS DateTime))
SET IDENTITY_INSERT [dbo].[PasswordForgot] OFF
SET IDENTITY_INSERT [dbo].[Permissions] ON 

INSERT [dbo].[Permissions] ([PermissionID], [PermissionName], [PermissionDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'Author Permissions', N'User can create, edit, and manage posts', N'11a53ca3-c86f-4e99-99d5-873f26c7f38e', CAST(N'2020-09-03T07:45:08.457' AS DateTime), CAST(N'2020-09-03T07:45:08.457' AS DateTime))
INSERT [dbo].[Permissions] ([PermissionID], [PermissionName], [PermissionDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'Editor Permissions', N'User can can approve or reject posts', N'11a53ca3-c86f-4e99-99d5-873f26c7f38e', CAST(N'2020-09-03T07:45:08.457' AS DateTime), CAST(N'2020-09-03T07:45:08.457' AS DateTime))
INSERT [dbo].[Permissions] ([PermissionID], [PermissionName], [PermissionDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (3, N'Admin Permissions', N'User can manage app settings and other users', N'11a53ca3-c86f-4e99-99d5-873f26c7f38e', CAST(N'2020-09-03T07:45:08.457' AS DateTime), CAST(N'2020-09-03T07:45:08.457' AS DateTime))
SET IDENTITY_INSERT [dbo].[Permissions] OFF
SET IDENTITY_INSERT [dbo].[SiteDataLookup] ON 

INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (1, N'FacebookLink', N'text', NULL, N'Social', N'https://www.facebook.com/#/', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (2, N'YoutubeLink', N'text', NULL, N'Social', N'https://www.youtube.com/channel/#', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (3, N'TwitterLink', N'text', NULL, N'Social', N'https://twitter.com/#', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (4, N'AboutInfoFooter', N'textarea', NULL, N'SiteContent', N'ImpactGambia is your number one trusted source for Gambian News on Politics, the Economy, Social Issues, and more. We provide you with the latest Breaking News and Videos straight from The Gambia and Abroad.', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (5, N'AboutUsPage', N'textarea', NULL, N'SiteContent', N'<p><b><span style="font-family: &quot;Arial Black&quot;;">About ImpactGambia</span></b></p><p>ImpactGambia is your number one trusted source for Gambian News on Politics, the Economy, Social Issues, and more. We provide you with the latest Breaking News and Videos straight from The Gambia and Abroad.<br></p><p><span style="font-weight: initial;">ImpactGambia</span>&nbsp;aims to provide Gambians with a platform to provide fair and objective coverage of the social, cultural, political, economic situation of The Gambia.&nbsp;</p><p>It aims to provide broad, journalistic coverage to the Gambian public.&nbsp;<span style="font-weight: initial;">ImpactGambia</span>&nbsp;puts a great deal of focus and emphasis on fact-checking, authenticity, and accuracy of data and information. Moreover, it serves as a medium platform for rational analytical thinking.&nbsp;</p><div><br>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (6, N'SiteLogoPath', N'text', NULL, N'SiteSetting', N'/assets/images/site/logo_banner_image.jpg', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (7, N'SiteLogoHeight', N'integer', NULL, N'SiteSetting', N'75', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (8, N'SiteLogoWidth', N'integer', NULL, N'SiteSetting', N'125', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (9, N'PayPalScript', N'textarea', NULL, N'SiteScript', N'<div class="jumbotron text-center">Your Paypal Butto Code</div>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (10, N'ShowAdverts', N'select', N'true,false', N'SiteSetting', N'false', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (11, N'ShowAbout', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (12, N'ShowWriteForUs', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (13, N'ShowJobs', N'select', N'true,false', N'SiteSetting', N'false', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (14, N'ShowDonation', N'select', N'true,false', N'SiteSetting', N'false', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (15, N'ShowWeatherWidget', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (16, N'ShowForexWidget', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (17, N'ShowCovidWidget', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (18, N'ShowAdSense', N'select', N'true,false', N'SiteSetting', N'false', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (19, N'AdSenseScript', N'textarea', NULL, N'SiteScript', N'<span>Your AdSense Script</span>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (20, N'LatestNewsCategory', N'text', NULL, N'SiteSetting', N'Latest', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (21, N'EntertainmentCategory', N'text', NULL, N'SiteSetting', N'Entertainment', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (22, N'MusicCategory', N'text', NULL, N'SiteSetting', N'Music', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (23, N'CelebrityCategory', N'text', NULL, N'SiteSetting', N'CelebrityNews', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (24, N'ShowAllNews', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (25, N'ShowLatestNews', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (44, N'ShareThisUrl', N'text', NULL, N'SiteSetting', N'https://platform-api.sharethis.com/js/sharethis.js#property=5f88d84e62a4f3001224d83c&product=sop', CAST(N'2020-11-30T02:28:19.263' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (27, N'SiteLogoHeightSmall', N'integer', NULL, N'SiteSetting', N'50', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (28, N'SiteLogoWidthSmall', N'integer', NULL, N'SiteSetting', N'100', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (29, N'SiteEmail', N'text', NULL, N'SiteInfo', N'info@impactgambia.net', CAST(N'2020-11-18T06:33:54.707' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (30, N'SiteNumber', N'text', NULL, N'SiteInfo', N'220 0000000', CAST(N'2020-11-18T06:34:26.250' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (32, N'SiteAddress', N'text', NULL, N'SiteInfo', N'The Gambia', CAST(N'2020-11-18T06:35:52.040' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (33, N'LiveNewsID', N'text', NULL, N'SiteSetting', N'sPgqEHsONK8', CAST(N'2020-11-18T08:30:13.687' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (34, N'AppDomain', N'text', NULL, N'SiteSetting', N'https://impactgambia.net', CAST(N'2020-11-30T01:01:58.300' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (35, N'DefaultPostApproveStatus', N'integer', NULL, N'SiteSetting', N'1', CAST(N'2020-11-30T01:09:23.323' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (36, N'UploadImageDefaultHeight', N'integer', NULL, N'SiteSetting', N'820', CAST(N'2020-11-30T01:13:00.677' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (37, N'UploadImageDefaultWidth', N'integer', NULL, N'SiteSetting', N'1450', CAST(N'2020-11-30T01:13:28.253' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (38, N'MaxGalleryImages', N'integer', NULL, N'SiteSetting', N'12', CAST(N'2020-11-30T01:23:38.080' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (39, N'TextWaterMark', N'text', NULL, N'SiteSetting', N'https://impactgambia.net', CAST(N'2020-11-30T01:26:46.363' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (40, N'ImageWaterMark', NULL, NULL, N'SiteSetting', N'impact-gambia.png', CAST(N'2020-11-30T01:27:35.217' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (41, N'CheckProfileCompletion', N'select', N'true,false', N'SiteSetting', N'false', CAST(N'2020-11-30T01:27:35.217' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (42, N'GoogleAnalyticsID', N'text', NULL, N'SiteSetting', N'UA-180879812-1', CAST(N'2020-11-30T01:51:14.080' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (43, N'GoogleAnalyticsLink', N'text', NULL, N'SiteSetting', N'https://analytics.google.com/analytics/web/#/report-home/a180879812w249809717p231471710', CAST(N'2020-11-30T01:51:48.297' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (46, N'EnableFaceBookComments', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-30T02:42:57.297' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (47, N'FacebookCommentAppId', N'text', NULL, N'SiteSetting', N'2015587022070357', CAST(N'2020-11-30T02:43:48.197' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (48, N'SiteName', N'text', NULL, N'SiteInfo', N'Impact Gambia', CAST(N'2021-01-12T11:04:04.593' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (49, N'ShowTwitterFeed', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (50, N'TwitterFeed', N'text', NULL, N'SiteScript', N'<a class="twitter-timeline" href="https://twitter.com/impact_gambia?ref_src=twsrc%5Etfw">Tweets by impact_gambia</a> <script async src="https://platform.twitter.com/widgets.js" charset="utf-8"></script>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (51, N'ShowFacebookFeed', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (60, N'MetaKeywords', N'text', NULL, N'SEO', N'Gambia News, Africa News, World News, ImpactGambia, Impact Gambia News, impactgambia.net, The Gambia, Gambia Currency, Gambia Population, Gambian Tourism,  Gambia Holidays, Gambia Coronavirus, Adama Barrow, TRRC, TRRC Gambia Today, Banta Keita, Ismaila Ceesay, Ousianou Darboe, Gambias COVID-19, The Gambia Tourism', CAST(N'2021-01-31T18:34:20.947' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (61, N'MetaTitle', N'text', NULL, N'SEO', N'Gambia News - Impact Gambia  | Trusted news from The Gambia', CAST(N'2021-01-31T19:36:41.700' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (62, N'MetaDescription', N'text', NULL, N'SEO', N'ImpactGambia is your number one trusted souce for Gambian News on Politics, the Economy, Social Issues, and more. We provide you with the latest Breaking News and Videos straight from The Gambia and Abroad.', CAST(N'2021-01-31T19:39:19.470' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (63, N'SupportEmail', N'text', NULL, N'SiteInfo', N'info@impactgambia.net', CAST(N'2021-01-31T19:58:41.717' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (64, N'WeatherWidget', N'text', NULL, N'SiteScript', N'<a target="_blank" href="https://www.booked.net/weather/banjul-14443"><img src="https://w.bookcdn.com/weather/picture/32_14443_1_1_34495e_250_2c3e50_ffffff_ffffff_1_2071c9_ffffff_0_6.png?scode=2&domid=w209&anc_id=713" alt="booked.net" /></a>', CAST(N'2021-01-31T20:12:57.680' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (52, N'FacebookFeed', N'text', NULL, N'SiteScript', N'<script></script>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (53, N'ShowInstagramFeed', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (54, N'InstagramFeed', N'text', NULL, N'SiteScript', N'<script></script>', CAST(N'2020-11-13T02:59:46.380' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (55, N'InstagramLink', N'text', NULL, N'Social', N'https://www.instagram.com/impactgambia/', CAST(N'2021-01-30T07:45:34.187' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (56, N'NewsApiKey', N'text', NULL, N'SiteSetting', N'5593437c832f4060a2a5f501dc72474a', CAST(N'2021-01-30T18:58:42.290' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (57, N'ShowNewsApi', N'select', N'true,false', N'SiteSetting', N'true', CAST(N'2021-01-30T19:01:05.477' AS DateTime))
INSERT [dbo].[SiteDataLookup] ([ID], [UinqueKey], [DataType], [DataOptions], [DataGroup], [Value], [DateAdded]) VALUES (59, N'CovidWidget', N'text', NULL, N'SiteScript', N'<iframe src="https://public.domo.com/cards/dJ45D" width="100%" height="600" marginheight="0" marginwidth="0" frameborder="0"></iframe>', CAST(N'2021-01-31T02:43:01.223' AS DateTime))
SET IDENTITY_INSERT [dbo].[SiteDataLookup] OFF
SET IDENTITY_INSERT [dbo].[Subscribers] ON 

INSERT [dbo].[Subscribers] ([ID], [SubscriberID], [FullName], [SubscriberEmail], [Password], [EmailVerification], [ProfilePicture], [DateSubscribed]) VALUES (1, N'557ad23b-8d06-40b8-9e15-7280b0603101', NULL, N'akassama@yahoo.com', NULL, 0, NULL, CAST(N'2021-01-13T12:20:43.843' AS DateTime))
SET IDENTITY_INSERT [dbo].[Subscribers] OFF
SET IDENTITY_INSERT [dbo].[Tags] ON 

INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'a12a8bd0-cfaa-4195-aff3-92b3f28a043f', N'News', N'News', N'News tag', N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'52e273af-e17a-40ca-a9b8-81290ea904d7', N'Politics', N'Politics', N'Politics tag', N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (3, N'5454f17d-7c86-4fb2-81cd-c16eb0325980', N'International', N'International', N'International tag', N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (4, N'81dc3d59-edc1-4c62-bd64-4b70c0864336', N'Business', N'Business', N'Business tag', N'e1e1a3ee-8956-4fa4-82c6-28c9d3102f65', CAST(N'2020-11-13T02:59:49.643' AS DateTime), CAST(N'2020-11-13T02:59:49.643' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (5, N'70c6fc6e-3146-440b-98be-86774c90de3c', N'The Gambia', N'TheGambia', N'The Gambia', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-13T06:17:35.507' AS DateTime), CAST(N'2020-11-13T06:17:35.463' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (6, N'c912e66e-0a35-4a78-b613-7940e7be916d', N'Covid19', N'Covid19', N'Covid 19', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:19:29.217' AS DateTime), CAST(N'2020-11-16T23:19:29.070' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (7, N'05a35786-d956-45af-9ece-1714bda86527', N'Celebrity News', N'CelebrityNews', N'Celebrity News', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:19:47.107' AS DateTime), CAST(N'2020-11-16T23:19:47.103' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (8, N'35b8ed22-8cd1-443c-845f-266bc0ca86e3', N'Music', N'Music', N'Music', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:20:08.287' AS DateTime), CAST(N'2020-11-16T23:20:08.273' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (9, N'75268a01-a551-408e-a3be-29c732ae5d15', N'Video', N'Video', N'Video', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:20:31.227' AS DateTime), CAST(N'2020-11-16T23:20:31.207' AS DateTime))
INSERT [dbo].[Tags] ([ID], [TagID], [TagName], [ShortTagName], [TagDescription], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (10, N'8e03288c-16c9-494c-a33b-8252ffa0b96c', N'Entertainment', N'Entertainment', N'Entertainment', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:20:45.420' AS DateTime), CAST(N'2020-11-16T23:20:45.413' AS DateTime))
SET IDENTITY_INSERT [dbo].[Tags] OFF
SET IDENTITY_INSERT [dbo].[TopTenList] ON 

INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (1, N'066a8fe3-0652-4b8f-80f4-b00e2094669b', N'MusicVideos', N'Hussain Dada Ringa Roses x Uchee & Chanta

', 1, N'https://www.youtube.com/watch?v=PiWTfnSWN5M', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (2, N'4b64ec67-ad1c-4f9a-8d94-5e3f8e8819a6', N'MusicVideos', N'	ST GAMBIAN DREAM - KOKOLIKO', 2, N'https://www.youtube.com/watch?v=8VoxKCTG9xQ', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (3, N'1bb462be-6b6d-488f-a13f-56c614ef99b8', N'MusicVideos', N'Jizzle - Te Amo', 3, N'https://www.youtube.com/watch?v=VCUc9JkCqUk', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (4, N'545d5596-f8c3-42c9-9953-79d6cb995532', N'MusicVideos', N'ST Da Gambian Dream - JATOO

', 4, N'https://www.youtube.com/watch?v=AG99IXPDoro', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (5, N'7184e533-315e-44c8-81ba-5fb0f335a489', N'MusicVideos', N'Jizzle - Turn By Turn', 5, N'https://www.youtube.com/watch?v=-Ix8nwQSFH4', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (6, N'fdc2f9f5-73f8-4357-b74f-c66a950fdc87', N'MusicVideos', N'ST Gambian Dream- KODO BAY KODO', 6, N'https://www.youtube.com/watch?v=faVvWbvfrlc', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (7, N'ea099757-d876-4c84-867e-b00561cf7237', N'MusicVideos', N'NOBLES GAMBIA Feat JALIBA KUYATEH—GAMBIA YOOLOLA', 7, N'https://www.youtube.com/watch?v=yHIGTxp7mSU', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (8, N'fbbdffd1-7cd5-4258-8e0d-556faf08f79a', N'MusicVideos', N'Jizzle - Incase', 8, N'https://www.youtube.com/watch?v=zEQvjB-EMqI', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (9, N'9fd8da22-edd7-4103-bc80-8145b6738c6f', N'MusicVideos', N'ST Da Gambian Dream - JATOO
', 9, N'https://www.youtube.com/watch?v=AG99IXPDoro', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
INSERT [dbo].[TopTenList] ([ID], [ListID], [ListType], [ListTitle], [ListOrder], [ListLink], [UpdatedBy], [UpdateDate], [DateAdded]) VALUES (10, N'bc27f8f6-26a6-4a3a-a4d7-6cff6523ad0f', N'MusicVideos', N'Big banga Ayonko ft hussian dada', 10, N'https://www.youtube.com/watch?v=kZoSdFMVYDQ', N'7b54f4f8-465d-4458-b3af-b835ea3a3a86', CAST(N'2020-11-16T23:50:16.937' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[TopTenList] OFF
SET IDENTITY_INSERT [dbo].[VisitLogs] ON 

INSERT [dbo].[VisitLogs] ([ID], [LogType], [PageTitle], [IpAddress], [Country], [Browser], [Device], [VisitTime], [VisitDate], [OtherDetails]) VALUES (1, N'Home Page', NULL, N'127.0.0.1', NULL, N'Chrome', N'Desktop', NULL, CAST(N'2021-01-12T16:02:33.567' AS DateTime), N'Desktop')
SET IDENTITY_INSERT [dbo].[VisitLogs] OFF
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_account_details_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[AccountDetails] ADD  CONSTRAINT [unique_account_details_id] UNIQUE NONCLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_account_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [unique_account_id] UNIQUE NONCLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_email]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Accounts] ADD  CONSTRAINT [unique_email] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_account_to_permission]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[AccountToPermission] ADD  CONSTRAINT [unique_account_to_permission] UNIQUE NONCLUSTERED 
(
	[AccountID] ASC,
	[PermissionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_advert_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Adverts] ADD  CONSTRAINT [unique_advert_id] UNIQUE NONCLUSTERED 
(
	[AdvertID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_author_subscriber_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[AuthorsSubscription] ADD  CONSTRAINT [unique_author_subscriber_id] UNIQUE NONCLUSTERED 
(
	[SubscriberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_category_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [unique_category_id] UNIQUE NONCLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_category_name]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [unique_category_name] UNIQUE NONCLUSTERED 
(
	[CategoryName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_category_subscriber_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[CategoriesSubscription] ADD  CONSTRAINT [unique_category_subscriber_id] UNIQUE NONCLUSTERED 
(
	[SubscriberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_embed_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[EmbeddedMusic] ADD  CONSTRAINT [unique_embed_id] UNIQUE NONCLUSTERED 
(
	[EmbedID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_image_link]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[GalleryImages] ADD  CONSTRAINT [unique_image_link] UNIQUE NONCLUSTERED 
(
	[ImageLink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_job_advert_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Jobs] ADD  CONSTRAINT [unique_job_advert_id] UNIQUE NONCLUSTERED 
(
	[JobID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_viewer_contraints]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[MessageViews] ADD  CONSTRAINT [unique_viewer_contraints] UNIQUE NONCLUSTERED 
(
	[MessageID] ASC,
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_reset_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[PasswordForgot] ADD  CONSTRAINT [unique_reset_id] UNIQUE NONCLUSTERED 
(
	[ResetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_permission_name]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Permissions] ADD  CONSTRAINT [unique_permission_name] UNIQUE NONCLUSTERED 
(
	[PermissionName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_posts_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[PostApprovals] ADD  CONSTRAINT [unique_posts_id] UNIQUE NONCLUSTERED 
(
	[PostID] ASC,
	[PostType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_post_permalink]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [unique_post_permalink] UNIQUE NONCLUSTERED 
(
	[PostPermalink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_data_contraints]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[SiteDataLookup] ADD  CONSTRAINT [unique_data_contraints] UNIQUE NONCLUSTERED 
(
	[UinqueKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_account_social_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[SocialMediaDetails] ADD  CONSTRAINT [unique_account_social_id] UNIQUE NONCLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_subscriber_email]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Subscribers] ADD  CONSTRAINT [unique_subscriber_email] UNIQUE NONCLUSTERED 
(
	[SubscriberEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_tag_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Tags] ADD  CONSTRAINT [unique_tag_id] UNIQUE NONCLUSTERED 
(
	[TagID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_tag_name]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[Tags] ADD  CONSTRAINT [unique_tag_name] UNIQUE NONCLUSTERED 
(
	[TagName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_tag_subscriber_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[TagsSubscription] ADD  CONSTRAINT [unique_tag_subscriber_id] UNIQUE NONCLUSTERED 
(
	[SubscriberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_list_id]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[TopTenList] ADD  CONSTRAINT [unique_list_id] UNIQUE NONCLUSTERED 
(
	[ListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [unique_video_link]    Script Date: 2/10/2021 7:03:47 PM ******/
ALTER TABLE [dbo].[VideoUploads] ADD  CONSTRAINT [unique_video_link] UNIQUE NONCLUSTERED 
(
	[VideoLink] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT ((0)) FOR [PhoneNumberVerification]
GO
ALTER TABLE [dbo].[AccountDetails] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT ((0)) FOR [EmailVerification]
GO
ALTER TABLE [dbo].[Accounts] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[AccountToPermission] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[ActivityLogs] ADD  CONSTRAINT [DF__ActivityL__Activ__4F7CD00D]  DEFAULT (getdate()) FOR [ActivityDate]
GO
ALTER TABLE [dbo].[Adverts] ADD  CONSTRAINT [DF__Adverts__DateAdd__6CA31EA0]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[AuthorsSubscription] ADD  DEFAULT (getdate()) FOR [DateSubscribed]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF__Categorie__IsPub__1DB06A4F]  DEFAULT ((0)) FOR [IsPublished]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF__Categorie__IsHea__1EA48E88]  DEFAULT ((0)) FOR [IsHeader]
GO
ALTER TABLE [dbo].[Categories] ADD  CONSTRAINT [DF__Categorie__DateA__1F98B2C1]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[CategoriesSubscription] ADD  DEFAULT (getdate()) FOR [DateSubscribed]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (NULL) FOR [Name]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (NULL) FOR [Email]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (NULL) FOR [Phone]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (NULL) FOR [Subject]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (NULL) FOR [Message]
GO
ALTER TABLE [dbo].[ContactMessages] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (NULL) FOR [ISO3]
GO
ALTER TABLE [dbo].[Country] ADD  DEFAULT (NULL) FOR [NumCode]
GO
ALTER TABLE [dbo].[EmbeddedMusic] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[GalleryImages] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Jobs] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[PasswordForgot] ADD  DEFAULT (getdate()) FOR [ResetDate]
GO
ALTER TABLE [dbo].[Permissions] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[PostApprovals] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[PostReviews] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF__Posts__IsBreakin__3493CFA7]  DEFAULT ((0)) FOR [IsBreakingNews]
GO
ALTER TABLE [dbo].[Posts] ADD  CONSTRAINT [DF__Posts__DateAdded__3587F3E0]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[PostViews] ADD  CONSTRAINT [DF__PostViews__Visit__395884C4]  DEFAULT (getdate()) FOR [VisitDate]
GO
ALTER TABLE [dbo].[SiteDataLookup] ADD  CONSTRAINT [DF__SiteDataL__DateA__7CD98669]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[SiteStats] ADD  DEFAULT (getdate()) FOR [ActionDate]
GO
ALTER TABLE [dbo].[SocialMediaDetails] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[Subscribers] ADD  DEFAULT ((0)) FOR [EmailVerification]
GO
ALTER TABLE [dbo].[Subscribers] ADD  DEFAULT (getdate()) FOR [DateSubscribed]
GO
ALTER TABLE [dbo].[Tags] ADD  CONSTRAINT [DF__Tags__DateAdded__619B8048]  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[TagsSubscription] ADD  DEFAULT (getdate()) FOR [DateSubscribed]
GO
ALTER TABLE [dbo].[VideoUploads] ADD  DEFAULT (getdate()) FOR [DateAdded]
GO
ALTER TABLE [dbo].[VisitLogs] ADD  DEFAULT (getdate()) FOR [VisitDate]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "PostViews"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 12
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPopularThisWeek'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPopularThisWeek'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "vwPosts"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PostReviews"
            Begin Extent = 
               Top = 6
               Left = 248
               Bottom = 136
               Right = 428
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPostReviews'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPostReviews'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[31] 4[21] 2[31] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Posts"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 220
               Right = 229
            End
            DisplayFlags = 280
            TopColumn = 7
         End
         Begin Table = "PostApprovals"
            Begin Extent = 
               Top = 0
               Left = 248
               Bottom = 231
               Right = 523
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 24
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPosts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPosts'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[12] 4[49] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "vwPosts"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 239
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPostsApproved'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'vwPostsApproved'
GO
USE [master]
GO
ALTER DATABASE [NET_NEWS_CORE] SET  READ_WRITE 
GO
