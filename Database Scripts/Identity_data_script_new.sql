USE [quZONE]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'13d112dc-399d-434a-9ee5-40b1d1cb3773', N'admin')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'9d648d57-d72b-4a66-a49f-80a042c72a6a', N'guest')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2472b528-e7d5-4671-ab11-0c195e9b0a35', N'manager')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'a24a7943-e4ab-46b4-9f43-a38c034035f9', N'staff')
SET IDENTITY_INSERT [dbo].[UserProfiles] ON 

INSERT [dbo].[UserProfiles] ([Id], [OrgainzationId], [PositionId], [AvatarImgUrl], [AvatarImgUrlSm], [AvatarImgUrlMd], [ContactEmail]) VALUES (1, 1, 1, N'content/images/avatars/admin.jpg', NULL, NULL, N'admin@quzone.com')

SET IDENTITY_INSERT [dbo].[UserProfiles] OFF

INSERT [dbo].[AspNetUsers] ([Id], [FirstName], [LastName], [Level], [JoinDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [UserProfile_Id]) VALUES (N'3aa038b3-deae-4883-9592-0d2c54a9af34', N'Admin', N'Admin', 3, CAST(N'2016-04-19T00:00:00.000' AS DateTime), N'admin@ezq.com', 1, N'AMyC4ysJjyYqdGJrMIT46BypCVMnZRSyruDa5290pUP156WoMOC06hbtPIV4wfVZgA==', N'c52801f1-694f-40fb-9171-ff3c999cb6fc', NULL, 0, 0, NULL, 0, 0, N'admin', 1)

INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'3aa038b3-deae-4883-9592-0d2c54a9af34', N'13d112dc-399d-434a-9ee5-40b1d1cb3773')

