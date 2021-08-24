USE [master]
GO
/****** Object:  Database [NuxibaTestArchDB]    Script Date: 24/08/2021 01:26:38 a. m. ******/
CREATE DATABASE [NuxibaTestArchDB]
GO
USE [NuxibaTestArchDB]
GO
/****** Object:  Table [dbo].[Sexo]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sexo](
	[Id] [tinyint] NOT NULL,
	[Descripcion] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Sexo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[CorreoElectronico] [varchar](50) NOT NULL,
	[Username] [varchar](25) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[Estatus] [bit] NOT NULL,
	[Sexo] [tinyint] NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Nombre] [varchar](150) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwUsuario]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vwUsuario]
AS
SELECT        U.Id, U.Username, U.CorreoElectronico, S.Descripcion AS Sexo, (CASE U.[Estatus] WHEN 1 THEN 'SI' ELSE 'NO' END) AS Estatus, U.Nombre
FROM            dbo.Usuario AS U INNER JOIN
                         dbo.Sexo AS S ON U.Sexo = S.Id
GO
/****** Object:  Table [dbo].[Tarea]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tarea](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](250) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Tarea] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Sexo] ([Id], [Descripcion]) VALUES (1, N'Masculino')
GO
INSERT [dbo].[Sexo] ([Id], [Descripcion]) VALUES (2, N'Femenino')
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 
GO
INSERT [dbo].[Usuario] ([Id], [CorreoElectronico], [Username], [Password], [Estatus], [Sexo], [FechaCreacion], [Nombre]) VALUES (9, N'usuario1@gmail.com', N'usuario1', N'w0gfgzJ3Cd4nFtZVHl4JC3GlGMuJF1AfxNTtUuSlv0U=', 1, 1, CAST(N'2018-10-02T02:40:03.420' AS DateTime), N'')
GO
INSERT [dbo].[Usuario] ([Id], [CorreoElectronico], [Username], [Password], [Estatus], [Sexo], [FechaCreacion], [Nombre]) VALUES (10, N'usuario2@gmail.com', N'usuario2', N'LToYp30u90u2NRXm2tbKAo0VJcMPyj6f0gkfvlBaujw=', 1, 2, CAST(N'2018-10-02T02:42:53.760' AS DateTime), N'')
GO
INSERT [dbo].[Usuario] ([Id], [CorreoElectronico], [Username], [Password], [Estatus], [Sexo], [FechaCreacion], [Nombre]) VALUES (11, N'usuario3@gmail.com', N'Usuario3', N'9w0oLFgw2g47puQ/cBNT5eusGVVS5WJFczrmkHa+03E=', 0, 2, CAST(N'2021-08-23T22:42:56.353' AS DateTime), N'')
GO
INSERT [dbo].[Usuario] ([Id], [CorreoElectronico], [Username], [Password], [Estatus], [Sexo], [FechaCreacion], [Nombre]) VALUES (12, N'usuario4@gmail.com', N'usuario4', N'9w0oLFgw2g47puQ/cBNT5eusGVVS5WJFczrmkHa+03E=', 1, 2, CAST(N'2021-08-23T23:30:38.363' AS DateTime), N'')
GO
INSERT [dbo].[Usuario] ([Id], [CorreoElectronico], [Username], [Password], [Estatus], [Sexo], [FechaCreacion], [Nombre]) VALUES (13, N'usuario4@gmail.com', N'usuario5', N'9w0oLFgw2g47puQ/cBNT5eusGVVS5WJFczrmkHa+03E=', 1, 2, CAST(N'2021-08-23T23:35:55.120' AS DateTime), N'Jose Carlos 1')
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
ALTER TABLE [dbo].[Tarea] ADD  CONSTRAINT [DF_Tarea_Estatus]  DEFAULT ((1)) FOR [Estatus]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Estatus]  DEFAULT ((1)) FOR [Estatus]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_FechaCreacion]  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Usuario] ADD  DEFAULT ('') FOR [Nombre]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Sexo] FOREIGN KEY([Sexo])
REFERENCES [dbo].[Sexo] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Sexo]
GO
/****** Object:  StoredProcedure [dbo].[spInsertTarea]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertTarea]
	@Nombre varchar(50),
	@Descripcion varchar(250),
	@FechaCreacion datetime,
	@Estatus bit
AS
BEGIN

    INSERT INTO [dbo].[Tarea]
           ([Nombre]
           ,[Descripcion]
		   ,[FechaCreacion]
           ,[Estatus])
     VALUES
           (@Nombre
           ,@Descripcion
		   ,@FechaCreacion
           ,@Estatus)

END
GO
/****** Object:  StoredProcedure [dbo].[spInsertUsuario]    Script Date: 24/08/2021 01:26:38 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spInsertUsuario]
	@CorreoElectronico varchar(50), 
	@Username varchar(25),
	@Nombre varchar(150),
	@Password varchar(250),
	@Estatus bit,
	@Sexo tinyint
AS
BEGIN

    INSERT INTO [dbo].[Usuario]
           ([CorreoElectronico]
           ,[Username]
		   ,[Nombre]
           ,[Password]
           ,[Estatus]
           ,[Sexo])
     VALUES
           (@CorreoElectronico
           ,@Username
		   ,@Nombre
           ,@Password
           ,@Estatus
           ,@Sexo)

END
GO