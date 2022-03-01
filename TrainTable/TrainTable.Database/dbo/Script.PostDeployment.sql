--- AspNetRoles
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) 
VALUES (N'0a36dc88-f474-4bb9-83fe-c9e513d13471', N'EventManager', N'EVENTMANAGER', N'1a0c4e3e-971f-49d5-a115-9120299a5d67'),
(N'f9cbb24a-d1c6-4317-aba5-d6f896c3f479', N'User', N'USER', N'd6478a1f-914e-47d8-b93b-a8c9dab510fc')

--- [AspNetUsers]
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName]) 
VALUES (N'2a895c9d-68e9-43a9-97d6-be503180140d', N'user@tut.by', N'USER@TUT.BY', N'user222@tut.by', N'USER222@TUT.BY', 0, N'AQAAAAEAACcQAAAAECDp1A4aCBVwPuJEwKt5fqdaGS/ihbhIaVWgCgjdQrMvlITAQ4Khckb4NtnOhFL3Mg==', N'EFWDXHJRSN75SBBOGUJWOMJVCTIC4LWR', N'7de292fe-5d5b-466d-b4dc-d53054a5b7d5', NULL, 0, 0, NULL, 1, 0, N'Vladimir Lenin'),
(N'ee1550e7-01fc-413e-82f5-4aae491102d7', N'test@tut.by', N'TEST@TUT.BY', N'test@tut.by', N'TEST@TUT.BY', 0, N'AQAAAAEAACcQAAAAELRulEWsuvYT9GwfRb6vNlmS8UAyRCI/sJzP8zINrNL7SkneebMX1IzuTTwYF8KZBQ==', N'FSCK2TZV2RRFJCYGRHVVLVGG5RE232WM', N'cbc87827-6a89-4083-a8c8-edf60b1724fd', NULL, 0, 0, NULL, 1, 0, N'George Washington'),
(N'f5357fa0-a697-4afe-9bb1-ee2694c5dceb', N'eventManager@tut.by', N'EVENTMANAGER@TUT.BY', N'eventManager@tut.by', N'EVENTMANAGER@TUT.BY', 0, N'AQAAAAEAACcQAAAAEGKNNRgJB+C2jSef3Gx2MNxzF7GANZbT38H3QDKO5UcOWh8JkfhikgCowpGhI6+X9g==', N'3HCODGGLMUURZ2EPDSA3IBVRCQO2SEQ6', N'a772f879-42d3-4272-ba18-db89864eb2df', NULL, 0, 0, NULL, 1, 0, N'Bill Gates')

--- AspNetRoles
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'f5357fa0-a697-4afe-9bb1-ee2694c5dceb', N'0a36dc88-f474-4bb9-83fe-c9e513d13471'),
(N'2a895c9d-68e9-43a9-97d6-be503180140d', N'f9cbb24a-d1c6-4317-aba5-d6f896c3f479'),
(N'ee1550e7-01fc-413e-82f5-4aae491102d7', N'f9cbb24a-d1c6-4317-aba5-d6f896c3f479')

--- City
Insert [dbo].City ([Id], [Name]) VALUES (1, N'Минск'),
(2, N'Гомель')

--- City
Insert [dbo].Train([Id], [Name], [TypeId], DepartureId, DepartureTime, DestinationId, DestinationTime) 
VALUES (1, N'Минск-Гомель', 1, 1, cast('2011-11-11 10:00' as datetime),2, cast('2011-11-11 14:00' as datetime)),
(2, N'Гомель-Минск', 1, 2, cast('2011-11-11 10:00' as datetime),1, cast('2014-11-11 10:00' as datetime))