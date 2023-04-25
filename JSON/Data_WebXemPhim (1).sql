
GO
/****** Object:  Table [dbo].[User]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
Go
CREATE TABLE [dbo].[User](
	[UserID] int NOT NULL,
	[UserName] nvarchar(100) NOT NULL,
	[PassWord] nvarchar(50) NOT NULL,
	[UserType] nvarchar(20) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED  
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReView](
	[ReviewID] int NOT NULL,
	[UserID] int NOT NULL, 
	[MovieID] int NOT NULL,
	[Date] Datetime NOT NULL,
	[Rate] float NOT NULL,
	[Content] nvarchar(max) NOT NULL,
 CONSTRAINT [PK_ReView] PRIMARY KEY CLUSTERED 
(
	[ReviewID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Director]   Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Director](
	[DirectorID] int NOT NULL,
	[DirectorName] nvarchar(25) NOT NULL,
	[Story] nvarchar(100) NULL,
 CONSTRAINT [PK_Director] PRIMARY KEY CLUSTERED 
(
	[DirectorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movie]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movie](
	[MovieID] int NOT NULL,
	[TypeID] int NULL,
	[MovieName] nvarchar(150) NOT NULL,
	[ReleaseDate] date NOT NULL,
	[PosterPath] nvarchar(100),
	[Duration] float,
	[AverageRating] float,
	ViewCount Bigint,
	[OverView] nvarchar(200),
	[Country] nvarchar(50),
	[Url_Video] nvarchar(50),
 CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED 
(
	[MovieID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teaser]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teaser](
	[TeaserID] int NOT NULL,
	[MovieID] int NOT NULL,
	[Name] nvarchar(25) NOT NULL,
	[Key] nvarchar(25) NOT NULL,
 CONSTRAINT [PK_Teaser] PRIMARY KEY CLUSTERED 
(
	[TeaserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TypeMovie]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TypeMovie](
	[TypeID] int NOT NULL,
	[Name] nvarchar(25) NOT NULL,
 CONSTRAINT [PK_TypeMovie] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Actor]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Actor](
	[ActorID] int NOT NULL,
	[ActorName] nvarchar(25) NULL,
	[Story] nvarchar(25) NULL,
	[Avartar] nvarchar(50) NULL,
 CONSTRAINT [PK_Actor] PRIMARY KEY CLUSTERED 
(
	[ActorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailUserMovieFavorite]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailUserMovieFavorite](
	[ID] int NOT NULL,
	[UserID] int NOT NULL,
	[MovieID] int NOT NULL,
 CONSTRAINT [PK_DetailUserMovieFavorite] PRIMARY KEY CLUSTERED  
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) 
GO
/****** Object:  Table [dbo].[DetailActorMovie]   Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailActorMovie](
	[ID] int NOT NULL,
	[MovieID] int NOT NULL,
	[ActorID] int NOT NULL,
 CONSTRAINT [PK_DetailActorMovie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Genres]    Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Genres](
	[GenresID] int NOT NULL,
	[GenresName] nvarchar(100) NOT NULL,
 CONSTRAINT [PK_Genres] PRIMARY KEY CLUSTERED 
(
	[GenresID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailGenresMovie]   Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailGenresMovie](
	[ID] int NOT NULL,
	[MovieID] int NOT NULL,
	[GenresID] int NOT NULL,
 CONSTRAINT [PK_DetailGenresMovie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
/****** Object:  Table [dbo].[DetailDirectorMovie]   Script Date: 10/2/2023 11:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailDirectorMovie](
	[ID] int NOT NULL,
	[MovieID] int NOT NULL,
	[DirectorID] int NOT NULL,
 CONSTRAINT [PK_DetailDirectorMovie] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
--Khóa ngoại bảng đánh giá
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Movie]
--Khóa ngoại bảng phim
GO
ALTER TABLE [dbo].[Movie]  WITH CHECK ADD  CONSTRAINT [FK_Movie_TypeMovie] FOREIGN KEY([TypeID])
REFERENCES [dbo].[TypeMovie] ([TypeID])
GO
ALTER TABLE [dbo].[Movie] CHECK CONSTRAINT [FK_Movie_TypeMovie]
--Khóa ngoại bảng teaser
GO
ALTER TABLE [dbo].[Teaser]  WITH CHECK ADD  CONSTRAINT [FK_Teaser_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[Teaser] CHECK CONSTRAINT [FK_Teaser_Movie]
--Khóa ngoại bảng chi tiết thể loại và phim
GO
ALTER TABLE [dbo].[DetailGenresMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailGenres_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[DetailGenresMovie] CHECK CONSTRAINT [FK_DetailGenres_Movie]
GO
ALTER TABLE [dbo].[DetailGenresMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailGenres_Genres] FOREIGN KEY([GenresID])
REFERENCES [dbo].[Genres] ([GenresID])
GO
ALTER TABLE [dbo].[DetailGenresMovie] CHECK CONSTRAINT [FK_DetailGenres_Genres]
--Khóa ngoại bảng chi tiết diễn viên và phim
GO
ALTER TABLE [dbo].[DetailActorMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailActor_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[DetailActorMovie] CHECK CONSTRAINT [FK_DetailActor_Movie]
GO
ALTER TABLE [dbo].[DetailActorMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailActor_Actor] FOREIGN KEY([ActorID])
REFERENCES [dbo].[Actor] ([ActorID])
GO
ALTER TABLE [dbo].[DetailActorMovie] CHECK CONSTRAINT [FK_DetailActor_Actor]
--Khóa ngoại bảng chi tiết đạo diễn và phim
GO
ALTER TABLE [dbo].[DetailDirectorMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailDirector_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[DetailDirectorMovie] CHECK CONSTRAINT [FK_DetailDirector_Movie]
GO
ALTER TABLE [dbo].[DetailDirectorMovie]  WITH CHECK ADD  CONSTRAINT [FK_DetailDirector_Director] FOREIGN KEY([DirectorID])
REFERENCES [dbo].[Director] ([DirectorID])
GO
ALTER TABLE [dbo].[DetailDirectorMovie] CHECK CONSTRAINT [FK_DetailDirector_Director]
--Khóa ngoại bảng chi tiết người dùng và phim yêu thích
GO
ALTER TABLE [dbo].[DetailUserMovieFavorite]  WITH CHECK ADD  CONSTRAINT [FK_DetailUserMovieFavorite_Movie] FOREIGN KEY([MovieID])
REFERENCES [dbo].[Movie] ([MovieID])
GO
ALTER TABLE [dbo].[DetailUserMovieFavorite] CHECK CONSTRAINT [FK_DetailUserMovieFavorite_Movie]
GO
ALTER TABLE [dbo].[DetailUserMovieFavorite]  WITH CHECK ADD  CONSTRAINT [FK_DetailUserMovieFavorite_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[DetailUserMovieFavorite] CHECK CONSTRAINT [FK_DetailUserMovieFavorite_User]

--Tạo các object
--CREATE TYPE PersonType AS TABLE  
--(  
--    FirstName VARCHAR(50),  
--    LastName VARCHAR(50),  
--    Age INT  
--);  
-- them bang hoa don
go
CREATE TABLE [dbo].[Bill](
	[IDBill] int NOT NULL,
	[UserID] int NOT NULL,
	[PaymentId] nvarchar(50) NOT NULL,
	[OrderId] nvarchar(50) NOT NULL,
	[Amount] nvarchar(20) NOT NULL,
	[Email] nvarchar(50) not null,
	[Number] nvarchar(20) not null,
	[TimePayment] datetime not null,
	[Status] bit,
	
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED  
(
	[IDBill] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
-- them khoa ngoại bảng hóa đơn
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_User]
go
--cách thêm dữ liệu vào bảng từ file json
insert into Movie (MovieID,PosterPath, MovieName, ReleaseDate, AverageRating, OverView)
select read_value.* from openrowset (BULK 'C:\JSON\movie.json', single_clob) as json_import
	cross apply openjson(BulkColumn)
with(
	results nvarchar(max) as json
) as result cross apply openjson(results)
with(
	id int,
	poster_path nvarchar(50),
	title nvarchar(100),
	release_date nvarchar(20),
	vote_average float,
	overview nvarchar(200)
) as read_value
--Thêm dữ liệu bảng thể loại
insert into Genres(GenresID, GenresName)
select list.* from openrowset (BULK 'C:\JSON\genres.json', single_clob) as json_import
	cross apply openjson(BulkColumn)
with(
	genres nvarchar(max) '$.genres' as json
) as Genres cross apply openjson(genres)
with(
	id int '$.id',
	name nvarchar(50) '$.name'
) list

delete from Genres
update movie
set TypeID = 1, Duration = 0, ViewCount = 0, Country = N'Unknown', Url_Video = N'Unknown'

-- trigger thay doi usertype sau khi het han mua premium
--create trigger UpdateUserType ON [dbo].[User] AFTER INSERT, UPDATE as begin
--DECLARE @userId INT
--DECLARE @userType VARCHAR(50)
--DECLARE @paymentDate DATE
-- SELECT @userId = inserted.UserId,
go
CREATE TRIGGER tr_UpdateUserType
ON Bill
AFTER INSERT
AS
BEGIN
  DECLARE @userid int, @paymenttime datetime, @usertype nvarchar(20), @timeAdd int
  
  SELECT @userid = UserID, @paymenttime = TimePayment, @timeAdd = CAST(Amount AS INT)
  FROM inserted
  
  -- Thêm ngày vào thời điểm thanh toán
  SET @paymenttime = DATEADD(day, @timeAdd, @paymenttime)
  
  -- Kiểm tra xem đã hết thời gian chưa
  IF GETDATE() > @paymenttime
  BEGIN
    -- Nếu đã hết thời gian, cập nhật UserType trong bảng User
    SELECT @usertype = CASE UserType
                          WHEN '1' THEN '0'

                          ELSE UserType
                        END
    FROM [dbo].[User]
    WHERE UserID = @userid
    UPDATE [dbo].[User]
    SET UserType = @usertype
    WHERE UserID = @userid
  END
END

