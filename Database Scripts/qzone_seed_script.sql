USE [master]
GO
/****** Object:  Database [quZONE]    Script Date: 2017-02-18 10:59:33 AM ******/
CREATE DATABASE [quZONE]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'quZONE', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\quZONE.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'quZONE_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER16\MSSQL\DATA\quZONE_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [quZONE] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [quZONE].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [quZONE] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [quZONE] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [quZONE] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [quZONE] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [quZONE] SET ARITHABORT OFF 
GO
ALTER DATABASE [quZONE] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [quZONE] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [quZONE] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [quZONE] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [quZONE] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [quZONE] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [quZONE] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [quZONE] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [quZONE] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [quZONE] SET  DISABLE_BROKER 
GO
ALTER DATABASE [quZONE] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [quZONE] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [quZONE] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [quZONE] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [quZONE] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [quZONE] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [quZONE] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [quZONE] SET RECOVERY FULL 
GO
ALTER DATABASE [quZONE] SET  MULTI_USER 
GO
ALTER DATABASE [quZONE] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [quZONE] SET DB_CHAINING OFF 
GO
ALTER DATABASE [quZONE] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [quZONE] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [quZONE] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'quZONE', N'ON'
GO
ALTER DATABASE [quZONE] SET QUERY_STORE = OFF
GO
USE [quZONE]
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [quZONE]
GO
/****** Object:  User [qzone]    Script Date: 2017-02-18 10:59:34 AM ******/
CREATE USER [qzone] FOR LOGIN [qzone] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [qzone]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Address]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](150) NOT NULL,
	[AddressLine2] [nvarchar](150) NULL,
	[City] [nvarchar](50) NOT NULL,
	[ProvState] [nvarchar](50) NOT NULL,
	[PostZipCode] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[Level] [tinyint] NOT NULL,
	[JoinDate] [datetime] NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[UserProfile_Id] [int] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Guest]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guest](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[GuestFirstName] [nvarchar](150) NOT NULL,
	[GuestLastName] [nvarchar](150) NOT NULL,
	[GuestContactTel] [nvarchar](20) NOT NULL,
	[GuestContactEmail] [nvarchar](250) NULL,
	[IsPreferred] [bit] NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Guest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GuestTable]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GuestTable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableNumber] [nvarchar](5) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[TableSize] [nvarchar](50) NOT NULL,
	[MaxSeatsCapacity] [int] NOT NULL,
	[TableStatus] [nvarchar](50) NOT NULL,
	[LastUpdateTime] [datetime2](7) NOT NULL,
	[TableIconImgUrl] [nvarchar](250) NULL,
	[Notes] [nvarchar](450) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_GuestTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organization]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](350) NULL,
	[OrganizationType] [nvarchar](50) NOT NULL,
	[LogoImgUrl] [nvarchar](250) NULL,
	[LogoImgUrlSm] [nvarchar](250) NULL,
	[LogoImgUrlMd] [nvarchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[AddressId] [int] NOT NULL,
	[Telephone] [nvarchar](20) NULL,
	[MessageCode] [nvarchar](50) NULL,
	[Notes] [nvarchar](450) NULL,
	[CreateDate] [datetime] NOT NULL,
	[UpdateDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Organization] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Position]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Position](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PoistionTitle] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](450) NULL,
	[Notes] [nvarchar](350) NULL,
 CONSTRAINT [PK_Position] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[GuestId] [bigint] NOT NULL,
	[GuestGroupSize] [int] NOT NULL,
	[ChildrenAssistanceNeeded] [bit] NOT NULL,
	[RequestedStartDateTime] [nvarchar](50) NULL,
	[ReservationStatus] [nvarchar](50) NOT NULL,
	[ServedTime] [datetime2](7) NULL,
	[ServedTableNumber] [nvarchar](5) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
	[ReservedBy] [nvarchar](50) NULL,
	[Notes] [nvarchar](450) NULL,
 CONSTRAINT [PK_Reservation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserProfiles]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserProfiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrgainzationId] [int] NOT NULL,
	[PositionId] [int] NOT NULL,
	[AvatarImgUrl] [nvarchar](max) NULL,
	[AvatarImgUrlSm] [nvarchar](max) NULL,
	[AvatarImgUrlMd] [nvarchar](max) NULL,
	[ContactEmail] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.UserProfiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WalkInWaitList]    Script Date: 2017-02-18 10:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WalkInWaitList](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrganizationId] [int] NOT NULL,
	[GuestId] [bigint] NOT NULL,
	[ArrivalTime] [datetime2](7) NOT NULL,
	[GuestGroupSize] [int] NOT NULL,
	[WaitingStatus] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[StatusChangeTime] [datetime2](7) NOT NULL,
	[ServedTime] [datetime2](7) NOT NULL,
	[ServedTableNumber] [nvarchar](50) NULL,
	[Notes] [nvarchar](450) NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[UpdateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_WalkInWaitList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Address] ON 

INSERT [dbo].[Address] ([Id], [AddressLine1], [AddressLine2], [City], [ProvState], [PostZipCode]) VALUES (1, N'15686 107 Avenue', NULL, N'Surrey', N'BC', N'V4N 3H8')
SET IDENTITY_INSERT [dbo].[Address] OFF
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'13d112dc-399d-434a-9ee5-40b1d1cb3773', N'admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2472b528-e7d5-4671-ab11-0c195e9b0a35', N'manager')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9d648d57-d72b-4a66-a49f-80a042c72a6a', N'guest')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a24a7943-e4ab-46b4-9f43-a38c034035f9', N'staff')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3aa038b3-deae-4883-9592-0d2c54a9af34', N'13d112dc-399d-434a-9ee5-40b1d1cb3773')
INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Level], [JoinDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserProfile_Id]) VALUES (N'3aa038b3-deae-4883-9592-0d2c54a9af34', N'Admin', N'Admin', 3, CAST(N'2016-04-19T00:00:00.000' AS DateTime), N'admin@ezq.com', 1, N'AMyC4ysJjyYqdGJrMIT46BypCVMnZRSyruDa5290pUP156WoMOC06hbtPIV4wfVZgA==', N'c52801f1-694f-40fb-9171-ff3c999cb6fc', NULL, 0, 0, NULL, 0, 0, N'admin', 1)
SET IDENTITY_INSERT [dbo].[Organization] ON 

INSERT [dbo].[Organization] ([Id], [Name], [Description], [OrganizationType], [LogoImgUrl], [LogoImgUrlSm], [LogoImgUrlMd], [IsActive], [AddressId], [Telephone], [MessageCode], [Notes], [CreateDate], [UpdateDate]) VALUES (1, N'Skyware Systems Solutions', NULL, N'Technical Vendor', N'content/images/organizations/fs-logo-default.png', NULL, NULL, 1, 1, N'604-619-5810', NULL, NULL, CAST(N'2016-05-05T13:24:35.970' AS DateTime), CAST(N'2016-05-05T13:24:36.233' AS DateTime))
SET IDENTITY_INSERT [dbo].[Organization] OFF
SET IDENTITY_INSERT [dbo].[Position] ON 

INSERT [dbo].[Position] ([Id], [PoistionTitle], [Description], [Notes]) VALUES (1, N'Manager', NULL, NULL)
INSERT [dbo].[Position] ([Id], [PoistionTitle], [Description], [Notes]) VALUES (2, N'Staff', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Position] OFF
SET IDENTITY_INSERT [dbo].[UserProfiles] ON 

INSERT [dbo].[UserProfiles] ([Id], [OrgainzationId], [PositionId], [AvatarImgUrl], [AvatarImgUrlSm], [AvatarImgUrlMd], [ContactEmail]) VALUES (1, 1, 1, N'content/images/avatars/admin.jpg', NULL, NULL, N'admin@quzone.com')
SET IDENTITY_INSERT [dbo].[UserProfiles] OFF
ALTER TABLE [dbo].[Reservation] ADD  CONSTRAINT [DF_Reservation_BoosterNeeded]  DEFAULT ((0)) FOR [ChildrenAssistanceNeeded]
GO
ALTER TABLE [dbo].[WalkInWaitList] ADD  CONSTRAINT [DF_WalkInWaitList_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.UserProfiles_UserProfile_Id] FOREIGN KEY([UserProfile_Id])
REFERENCES [dbo].[UserProfiles] ([Id])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_dbo.AspNetUsers_dbo.UserProfiles_UserProfile_Id]
GO
ALTER TABLE [dbo].[GuestTable]  WITH CHECK ADD  CONSTRAINT [FK_GuestTable_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[GuestTable] CHECK CONSTRAINT [FK_GuestTable_Organization]
GO
ALTER TABLE [dbo].[Organization]  WITH CHECK ADD  CONSTRAINT [FK_Organization_Address] FOREIGN KEY([AddressId])
REFERENCES [dbo].[Address] ([Id])
GO
ALTER TABLE [dbo].[Organization] CHECK CONSTRAINT [FK_Organization_Address]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Guest] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guest] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Guest]
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD  CONSTRAINT [FK_Reservation_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[Reservation] CHECK CONSTRAINT [FK_Reservation_Organization]
GO
ALTER TABLE [dbo].[WalkInWaitList]  WITH CHECK ADD  CONSTRAINT [FK_WalkInWaitList_Guest] FOREIGN KEY([GuestId])
REFERENCES [dbo].[Guest] ([Id])
GO
ALTER TABLE [dbo].[WalkInWaitList] CHECK CONSTRAINT [FK_WalkInWaitList_Guest]
GO
ALTER TABLE [dbo].[WalkInWaitList]  WITH CHECK ADD  CONSTRAINT [FK_WalkInWaitList_Organization] FOREIGN KEY([OrganizationId])
REFERENCES [dbo].[Organization] ([Id])
GO
ALTER TABLE [dbo].[WalkInWaitList] CHECK CONSTRAINT [FK_WalkInWaitList_Organization]
GO
USE [master]
GO
ALTER DATABASE [quZONE] SET  READ_WRITE 
GO
