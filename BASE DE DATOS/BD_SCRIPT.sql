USE [master]
GO

CREATE DATABASE [GestionPersonalProinsa]
GO

USE [GestionPersonalProinsa]
GO

CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[APROBACIONSOLICITUD](
	[ID_APROBACIONSOLICITUD] [bigint] IDENTITY(1,1) NOT NULL,
	[FECHA_APROBACIONSOLICITUD] [datetime] NOT NULL,
	[SECUENCIA] [int] NULL,
	[RESPUESTA_SOLICITUD] [char](10) NOT NULL,
	[COMENTARIO] [varchar](200) NULL,
	[ENCARGADO_APROBACION_ID] [bigint] NOT NULL,
	[SOLICITUD_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_APROBACIONSOLICITUD_ID] PRIMARY KEY CLUSTERED 
(
	[ID_APROBACIONSOLICITUD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[CAPACITACION](
	[ID_CAPACION] [bigint] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [varchar](100) NULL,
	[INSTITUCION] [varchar](100) NULL,
	[LINK] [varchar](100) NULL,
	[CERTIFICACION] [varchar](100) NULL,
	[FECHA_FINALIZACION] [datetime] NULL,
	[CANTIDAD_HORAS] [int] NULL,
	[ESTADO] [bit] NULL,
 CONSTRAINT [PK_CAPACITACION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_CAPACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CARGO](
	[ID_CARGO] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_CARGO] [varchar](50) NULL,
	[DESCRIPCION] [varchar](200) NULL,
 CONSTRAINT [PK_CARGO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_CARGO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[CORREO](
	[ID_CORREO] [bigint] IDENTITY(1,1) NOT NULL,
	[CORREO] [varchar](255) NULL,
 CONSTRAINT [PK_CORREO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_CORREO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DB_ERRORES](
	[ErrorID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NULL,
	[ErrorNumber] [int] NULL,
	[ErrorState] [int] NULL,
	[ErrorSeverity] [int] NULL,
	[ErrorLine] [int] NULL,
	[ErrorProcedure] [varchar](max) NULL,
	[ErrorMessage] [varchar](max) NULL,
	[ErrorDateTime] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[DEDUCCION](
	[ID_DEDUCCION] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_INGRESO] [varchar](50) NULL,
	[DESCRIPCION] [varchar](200) NULL,
	[MONTO] [decimal](10, 2) NULL,
	[PORCENTAJE] [decimal](10, 2) NULL,
 CONSTRAINT [PK_DEDUCCION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_DEDUCCION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DEDUCCIONNOMINA](
	[ID_DEDUCIONNOMINA] [bigint] IDENTITY(1,1) NOT NULL,
	[MONTO] [decimal](10, 2) NULL,
	[DETALLE] [varchar](100) NULL,
	[DEDUCCION_ID] [bigint] NULL,
 CONSTRAINT [PK_DEDUCCIONNOMINA_ID] PRIMARY KEY CLUSTERED 
(
	[ID_DEDUCIONNOMINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DEPARTAMENTO](
	[ID_DEPARTAMENTO] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_DEPARTAMENTO] [varchar](50) NULL,
	[DESCRIPCION] [varchar](100) NOT NULL,
 CONSTRAINT [PK_DEPARTAMENTO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_DEPARTAMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DEPARTAMENTOSUPERVISOR](
	[ID_DEPARTAMENTOSUPERVISOR] [bigint] IDENTITY(1,1) NOT NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
	[DEPARTAMENTO_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_DEPARTAMENTOSUPERVISOR_ID] PRIMARY KEY CLUSTERED 
(
	[ID_DEPARTAMENTOSUPERVISOR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DIRRECCION](
	[ID_DIRECCION] [bigint] IDENTITY(1,1) NOT NULL,
	[DIRRECION] [varchar](600) NULL,
 CONSTRAINT [PK_DIRRECCION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_DIRECCION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMPLEADO](
	[ID_EMPLEADO] [bigint] IDENTITY(1,1) NOT NULL,
	[IDENTIFICACION] [int] NULL,
	[NOMBRECOMPLETO] [varchar](250) NULL,
	[FECHA_NACIMIENTO] [datetime] NULL,
	[FECHA_INGRESO] [datetime] NULL,
	[FOTO] [varchar](250) NULL,
	[SALARIO] [decimal](10, 2) NULL,
	[SALDO_VACACIONES] [int] NULL,
	[CARGO_ID] [bigint] NULL,
	[CORREO_ID] [bigint] NOT NULL,
	[HORARIOLABORAL_ID] [bigint] NULL,
	[DEPARTAMENTO_ID] [bigint] NULL,
 CONSTRAINT [PK_EMPLEADO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMPLEADOCAPACITACION](
	[ID_EMPLEADOCAPACITACION] [bigint] IDENTITY(1,1) NOT NULL,
	[CAPACITACION_ID] [bigint] NOT NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_EMPLEADOCAPACITACION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADOCAPACITACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMPLEADODIRRECCION](
	[ID_EMPLEADODIRRECCION] [bigint] IDENTITY(1,1) NOT NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
	[DIRRECION_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_EMPLEADODIRRECCION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADODIRRECCION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMPLEADODOCUMENTO](
	[ID_EMPLEADODOCUMENTO] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_DOCUMENTO] [varchar](50) NULL,
	[COMENTARIO] [varchar](200) NULL,
	[TIPODOCUMENTO_ID] [bigint] NULL,
	[EMPLEADO_ID] [bigint] NULL,
 CONSTRAINT [PK_EMPLEADODOCUMENTO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADODOCUMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[EMPLEADOTELEFONO](
	[ID_EMPLEADOTELEFONO] [bigint] IDENTITY(1,1) NOT NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
	[TELEFONO_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_EMPLEADOTELEFONO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_EMPLEADOTELEFONO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[FLUJOAPROBACION](
	[ID_FLUJOAPROBACION] [bigint] IDENTITY(1,1) NOT NULL,
	[SECUENCIA] [int] NOT NULL,
	[DEPARTAMENTO_APROBACION_ID] [bigint] NULL,
	[DEPARTAMENTO_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_FLUJOAPROBACION_ID] PRIMARY KEY CLUSTERED 
(
	[ID_FLUJOAPROBACION] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[HORARIOLABORAL](
	[ID_HORARIOLABORAL] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBREHL] [varchar](50) NOT NULL,
	[DESCRIPCION] [varchar](200) NULL,
	[HORA_INGRESO] [int] NULL,
	[HORA_SALIDA] [int] NULL,
 CONSTRAINT [PK_HORARIOLABORAL_ID] PRIMARY KEY CLUSTERED 
(
	[ID_HORARIOLABORAL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[INGRESO](
	[ID_INGRESO] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_INGRESO] [varchar](50) NOT NULL,
	[DESCRIPCION] [varchar](200) NULL,
	[MONTO] [decimal](10, 2) NULL,
	[PORCENTAJE] [decimal](10, 2) NULL,
 CONSTRAINT [PK_INGRESO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_INGRESO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[INGRESONOMINA](
	[ID_INGRESONOMINA] [bigint] IDENTITY(1,1) NOT NULL,
	[MONTO] [decimal](10, 2) NULL,
	[DETALLE] [varchar](100) NULL,
	[INGRESO_ID] [bigint] NULL,
 CONSTRAINT [PK_INGRESONOMINA_ID] PRIMARY KEY CLUSTERED 
(
	[ID_INGRESONOMINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[NOMINA](
	[ID_NOMINA] [bigint] IDENTITY(1,1) NOT NULL,
	[DESCRIPCION] [varchar](100) NULL,
	[PERIODO] [varchar](50) NULL,
	[FECHA_CALCULO] [datetime] NULL,
	[FECHA_PAGO] [datetime] NULL,
	[OBSERVACIONES] [varchar](500) NULL,
	[REVISIONES] [int] NULL,
	[FECHACREACION] [datetime] NULL,
	[FECHACAPROBACION] [datetime] NULL,
	[ESTADO] [bit] NULL,
	[TIPONOMINA_ID] [bigint] NULL,
	[CREADOR_ID] [bigint] NULL,
	[APROBADOR_ID] [bigint] NULL,
 CONSTRAINT [PK_NOMINA_ID] PRIMARY KEY CLUSTERED 
(
	[ID_NOMINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[NOMINADETALLE](
	[ID_NOMINADETALLE] [bigint] IDENTITY(1,1) NOT NULL,
	[HORAS_TRABAJADAS] [int] NULL,
	[HORAS_EXTRA] [int] NULL,
	[SALARIO_BRUTO] [decimal](10, 2) NULL,
	[SALARIO_NETO] [decimal](10, 2) NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
	[NOMINA_ID] [bigint] NOT NULL,
	[INGRESONOMINA_ID] [bigint] NOT NULL,
	[DEDUCCION_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_NOMINADETALLE_ID] PRIMARY KEY CLUSTERED 
(
	[ID_NOMINADETALLE] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[REGISTROACTIVIDAD](
	[ID_REGISTROACTIVIDAD] [bigint] IDENTITY(1,1) NOT NULL,
	[FECHA_INICIO] [datetime] NULL,
	[FECHA_FINAL] [datetime] NULL,
	[DETALLE] [varchar](500) NULL,
	[TIPOACTIVIDAD_ID] [bigint] NOT NULL,
	[EMPLEADO_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_REGISTROACTIVIDAD_ID] PRIMARY KEY CLUSTERED 
(
	[ID_REGISTROACTIVIDAD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SOLICITUD](
	[ID_SOLICITUD] [bigint] IDENTITY(1,1) NOT NULL,
	[FECHA_SOLICITUD] [datetime] NOT NULL,
	[FECHA_INICIO] [datetime] NOT NULL,
	[FECHA_FINAL] [datetime] NOT NULL,
	[COMENTARIO] [varchar](200) NULL,
	[DIAS] [int] NOT NULL,
	[DETALLE] [varchar](300) NULL,
	[ESTADO_SOLICITUD] [char](10) NULL,
	[SOLICITANTE_ID] [bigint] NOT NULL,
	[TIPOSOLICITUD_ID] [bigint] NOT NULL,
 CONSTRAINT [PK_SOLICITUD_ID] PRIMARY KEY CLUSTERED 
(
	[ID_SOLICITUD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TELEFONO](
	[ID_TELEFONO] [bigint] IDENTITY(1,1) NOT NULL,
	[TELEFONO] [varchar](22) NULL,
 CONSTRAINT [PK_TELEFONO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_TELEFONO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TIPOACTIVIDAD](
	[ID_TIPOACTIVIDAD] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_TIPO_CTIVIDAD] [varchar](50) NULL,
	[DESCRIPCION] [varchar](100) NULL,
 CONSTRAINT [PK_TIPOACTIVIDAD_ID] PRIMARY KEY CLUSTERED 
(
	[ID_TIPOACTIVIDAD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TIPODOCUMENTO](
	[ID_TIPODOCUMENTO] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_TIPODOCUMENTO] [varchar](50) NOT NULL,
	[DESCRIPCION] [varchar](200) NULL,
 CONSTRAINT [PK_TIPODOCUMENTO_ID] PRIMARY KEY CLUSTERED 
(
	[ID_TIPODOCUMENTO] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TIPONOMINA](
	[ID_TIPONOMINA] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_NOMINA] [varchar](50) NULL,
	[DESCRIPCION] [varchar](200) NULL,
 CONSTRAINT [PK_TIPONOMINA_ID] PRIMARY KEY CLUSTERED 
(
	[ID_TIPONOMINA] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TIPOSOLICITUD](
	[ID_TIPOSOLICITUD] [bigint] IDENTITY(1,1) NOT NULL,
	[NOMBRE_TIPO_SOLICITUD] [varchar](50) NULL,
	[DESCRIPCION] [varchar](100) NULL,
 CONSTRAINT [PK_TIPOSOLICITUD_ID] PRIMARY KEY CLUSTERED 
(
	[ID_TIPOSOLICITUD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'ADMINISTRADOR', NULL, NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'RECURSOS HUMANOS', NULL, NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'EMPLEADO', NULL, NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'36ba66b6-a40a-4444-8cdd-14e8b92f60c6', N'1')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'36ba66b6-a40a-4444-8cdd-14e8b92f60c6', N'tcamacho@proinsalat.com', N'TCAMACHO@PROINSALAT.COM', N'tcamacho@proinsalat.com', N'TCAMACHO@PROINSALAT.COM', 0, NULL, N'Z5YELD7SL32TKYYH655NXVMOANZFKBLD', N'73ec4b02-dd4a-4be5-b692-ad82b52b7274', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[CARGO] ON 

INSERT [dbo].[CARGO] ([ID_CARGO], [NOMBRE_CARGO], [DESCRIPCION]) VALUES (1, N'PRUEBA_CARGO', N'PRUEBA')
SET IDENTITY_INSERT [dbo].[CARGO] OFF
GO
SET IDENTITY_INSERT [dbo].[CORREO] ON 

INSERT [dbo].[CORREO] ([ID_CORREO], [CORREO]) VALUES (1, N'tcamacho@proinsalat.com')
SET IDENTITY_INSERT [dbo].[CORREO] OFF
GO
SET IDENTITY_INSERT [dbo].[DEPARTAMENTO] ON 

INSERT [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO], [NOMBRE_DEPARTAMENTO], [DESCRIPCION]) VALUES (4, N'PRUEBA', N'PRUEBA')
SET IDENTITY_INSERT [dbo].[DEPARTAMENTO] OFF
GO
SET IDENTITY_INSERT [dbo].[EMPLEADO] ON 

INSERT [dbo].[EMPLEADO] ([ID_EMPLEADO], [IDENTIFICACION], [NOMBRECOMPLETO], [FECHA_NACIMIENTO], [FECHA_INGRESO], [FOTO], [SALARIO], [SALDO_VACACIONES], [CARGO_ID], [CORREO_ID], [HORARIOLABORAL_ID], [DEPARTAMENTO_ID]) VALUES (1, NULL, N'TIFFANY CAMACHO MONGE', NULL, NULL, NULL, NULL, NULL, 1, 1, NULL, 4)
SET IDENTITY_INSERT [dbo].[EMPLEADO] OFF
GO
ALTER TABLE [dbo].[APROBACIONSOLICITUD]  WITH CHECK ADD  CONSTRAINT [FK_APROBACIONSOLICITUD_EMPLEADO] FOREIGN KEY([ENCARGADO_APROBACION_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[APROBACIONSOLICITUD] CHECK CONSTRAINT [FK_APROBACIONSOLICITUD_EMPLEADO]
GO
ALTER TABLE [dbo].[APROBACIONSOLICITUD]  WITH CHECK ADD  CONSTRAINT [FK_APROBACIONSOLICITUD_SOLICITUD] FOREIGN KEY([SOLICITUD_ID])
REFERENCES [dbo].[SOLICITUD] ([ID_SOLICITUD])
GO
ALTER TABLE [dbo].[APROBACIONSOLICITUD] CHECK CONSTRAINT [FK_APROBACIONSOLICITUD_SOLICITUD]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[DEDUCCIONNOMINA]  WITH CHECK ADD  CONSTRAINT [FK_DEDUCCIONNOMINA_DEDUCCION] FOREIGN KEY([DEDUCCION_ID])
REFERENCES [dbo].[DEDUCCION] ([ID_DEDUCCION])
GO
ALTER TABLE [dbo].[DEDUCCIONNOMINA] CHECK CONSTRAINT [FK_DEDUCCIONNOMINA_DEDUCCION]
GO
ALTER TABLE [dbo].[DEPARTAMENTOSUPERVISOR]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTAMENTOSUPERVISOR_DEPARTAMENTO] FOREIGN KEY([DEPARTAMENTO_ID])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[DEPARTAMENTOSUPERVISOR] CHECK CONSTRAINT [FK_DEPARTAMENTOSUPERVISOR_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[DEPARTAMENTOSUPERVISOR]  WITH CHECK ADD  CONSTRAINT [FK_DEPARTAMENTOSUPERVISOR_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[DEPARTAMENTOSUPERVISOR] CHECK CONSTRAINT [FK_DEPARTAMENTOSUPERVISOR_EMPLEADO]
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADO_CARGO] FOREIGN KEY([CARGO_ID])
REFERENCES [dbo].[CARGO] ([ID_CARGO])
GO
ALTER TABLE [dbo].[EMPLEADO] CHECK CONSTRAINT [FK_EMPLEADO_CARGO]
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADO_CORREO] FOREIGN KEY([CORREO_ID])
REFERENCES [dbo].[CORREO] ([ID_CORREO])
GO
ALTER TABLE [dbo].[EMPLEADO] CHECK CONSTRAINT [FK_EMPLEADO_CORREO]
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADO_DEPARTAMENTO] FOREIGN KEY([DEPARTAMENTO_ID])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[EMPLEADO] CHECK CONSTRAINT [FK_EMPLEADO_DEPARTAMENTO]
GO
ALTER TABLE [dbo].[EMPLEADO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADO_HORARIOLABORAL] FOREIGN KEY([HORARIOLABORAL_ID])
REFERENCES [dbo].[HORARIOLABORAL] ([ID_HORARIOLABORAL])
GO
ALTER TABLE [dbo].[EMPLEADO] CHECK CONSTRAINT [FK_EMPLEADO_HORARIOLABORAL]
GO
ALTER TABLE [dbo].[EMPLEADOCAPACITACION]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADOCAPACITACION_CAPACITACION] FOREIGN KEY([CAPACITACION_ID])
REFERENCES [dbo].[CAPACITACION] ([ID_CAPACION])
GO
ALTER TABLE [dbo].[EMPLEADOCAPACITACION] CHECK CONSTRAINT [FK_EMPLEADOCAPACITACION_CAPACITACION]
GO
ALTER TABLE [dbo].[EMPLEADOCAPACITACION]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADOCAPACITACION_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[EMPLEADOCAPACITACION] CHECK CONSTRAINT [FK_EMPLEADOCAPACITACION_EMPLEADO]
GO
ALTER TABLE [dbo].[EMPLEADODIRRECCION]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADODIRRECCION_DIRECCION] FOREIGN KEY([DIRRECION_ID])
REFERENCES [dbo].[DIRRECCION] ([ID_DIRECCION])
GO
ALTER TABLE [dbo].[EMPLEADODIRRECCION] CHECK CONSTRAINT [FK_EMPLEADODIRRECCION_DIRECCION]
GO
ALTER TABLE [dbo].[EMPLEADODIRRECCION]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADODIRRECCION_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[EMPLEADODIRRECCION] CHECK CONSTRAINT [FK_EMPLEADODIRRECCION_EMPLEADO]
GO
ALTER TABLE [dbo].[EMPLEADODOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADODOCUMENTO_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[EMPLEADODOCUMENTO] CHECK CONSTRAINT [FK_EMPLEADODOCUMENTO_EMPLEADO]
GO
ALTER TABLE [dbo].[EMPLEADODOCUMENTO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADODOCUMENTO_TIPODOCUMENTO] FOREIGN KEY([TIPODOCUMENTO_ID])
REFERENCES [dbo].[TIPODOCUMENTO] ([ID_TIPODOCUMENTO])
GO
ALTER TABLE [dbo].[EMPLEADODOCUMENTO] CHECK CONSTRAINT [FK_EMPLEADODOCUMENTO_TIPODOCUMENTO]
GO
ALTER TABLE [dbo].[EMPLEADOTELEFONO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADOTELEFONO_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[EMPLEADOTELEFONO] CHECK CONSTRAINT [FK_EMPLEADOTELEFONO_EMPLEADO]
GO
ALTER TABLE [dbo].[EMPLEADOTELEFONO]  WITH CHECK ADD  CONSTRAINT [FK_EMPLEADOTELEFONO_TELEFONO] FOREIGN KEY([TELEFONO_ID])
REFERENCES [dbo].[TELEFONO] ([ID_TELEFONO])
GO
ALTER TABLE [dbo].[EMPLEADOTELEFONO] CHECK CONSTRAINT [FK_EMPLEADOTELEFONO_TELEFONO]
GO
ALTER TABLE [dbo].[FLUJOAPROBACION]  WITH CHECK ADD  CONSTRAINT [FK_FLUJOAPROBACION_DEPARTAMENTO1] FOREIGN KEY([DEPARTAMENTO_APROBACION_ID])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[FLUJOAPROBACION] CHECK CONSTRAINT [FK_FLUJOAPROBACION_DEPARTAMENTO1]
GO
ALTER TABLE [dbo].[FLUJOAPROBACION]  WITH CHECK ADD  CONSTRAINT [FK_FLUJOAPROBACION_DEPARTAMENTO2] FOREIGN KEY([DEPARTAMENTO_ID])
REFERENCES [dbo].[DEPARTAMENTO] ([ID_DEPARTAMENTO])
GO
ALTER TABLE [dbo].[FLUJOAPROBACION] CHECK CONSTRAINT [FK_FLUJOAPROBACION_DEPARTAMENTO2]
GO
ALTER TABLE [dbo].[INGRESONOMINA]  WITH CHECK ADD  CONSTRAINT [FK_INGRESONOMINA_INGRESO] FOREIGN KEY([INGRESO_ID])
REFERENCES [dbo].[INGRESO] ([ID_INGRESO])
GO
ALTER TABLE [dbo].[INGRESONOMINA] CHECK CONSTRAINT [FK_INGRESONOMINA_INGRESO]
GO
ALTER TABLE [dbo].[NOMINA]  WITH CHECK ADD  CONSTRAINT [FK_NOMINA_EMPLEADO1] FOREIGN KEY([CREADOR_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[NOMINA] CHECK CONSTRAINT [FK_NOMINA_EMPLEADO1]
GO
ALTER TABLE [dbo].[NOMINA]  WITH CHECK ADD  CONSTRAINT [FK_NOMINA_EMPLEADO2] FOREIGN KEY([APROBADOR_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[NOMINA] CHECK CONSTRAINT [FK_NOMINA_EMPLEADO2]
GO
ALTER TABLE [dbo].[NOMINA]  WITH CHECK ADD  CONSTRAINT [FK_NOMINA_TIPONOMINA] FOREIGN KEY([TIPONOMINA_ID])
REFERENCES [dbo].[TIPONOMINA] ([ID_TIPONOMINA])
GO
ALTER TABLE [dbo].[NOMINA] CHECK CONSTRAINT [FK_NOMINA_TIPONOMINA]
GO
ALTER TABLE [dbo].[NOMINADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_NOMINADETALLE_DEDUCCIONNOMINA] FOREIGN KEY([DEDUCCION_ID])
REFERENCES [dbo].[DEDUCCIONNOMINA] ([ID_DEDUCIONNOMINA])
GO
ALTER TABLE [dbo].[NOMINADETALLE] CHECK CONSTRAINT [FK_NOMINADETALLE_DEDUCCIONNOMINA]
GO
ALTER TABLE [dbo].[NOMINADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_NOMINADETALLE_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[NOMINADETALLE] CHECK CONSTRAINT [FK_NOMINADETALLE_EMPLEADO]
GO
ALTER TABLE [dbo].[NOMINADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_NOMINADETALLE_INGRESONOMINA] FOREIGN KEY([INGRESONOMINA_ID])
REFERENCES [dbo].[INGRESONOMINA] ([ID_INGRESONOMINA])
GO
ALTER TABLE [dbo].[NOMINADETALLE] CHECK CONSTRAINT [FK_NOMINADETALLE_INGRESONOMINA]
GO
ALTER TABLE [dbo].[NOMINADETALLE]  WITH CHECK ADD  CONSTRAINT [FK_NOMINADETALLE_NOMINA] FOREIGN KEY([NOMINA_ID])
REFERENCES [dbo].[NOMINA] ([ID_NOMINA])
GO
ALTER TABLE [dbo].[NOMINADETALLE] CHECK CONSTRAINT [FK_NOMINADETALLE_NOMINA]
GO
ALTER TABLE [dbo].[REGISTROACTIVIDAD]  WITH CHECK ADD  CONSTRAINT [FK_REGISTROACTIVIDAD_EMPLEADO] FOREIGN KEY([EMPLEADO_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[REGISTROACTIVIDAD] CHECK CONSTRAINT [FK_REGISTROACTIVIDAD_EMPLEADO]
GO
ALTER TABLE [dbo].[REGISTROACTIVIDAD]  WITH CHECK ADD  CONSTRAINT [FK_REGISTROACTIVIDAD_TIPOACTIVIDAD] FOREIGN KEY([TIPOACTIVIDAD_ID])
REFERENCES [dbo].[TIPOACTIVIDAD] ([ID_TIPOACTIVIDAD])
GO
ALTER TABLE [dbo].[REGISTROACTIVIDAD] CHECK CONSTRAINT [FK_REGISTROACTIVIDAD_TIPOACTIVIDAD]
GO
ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD  CONSTRAINT [FK_SOLICITUD_EMPLEADO] FOREIGN KEY([SOLICITANTE_ID])
REFERENCES [dbo].[EMPLEADO] ([ID_EMPLEADO])
GO
ALTER TABLE [dbo].[SOLICITUD] CHECK CONSTRAINT [FK_SOLICITUD_EMPLEADO]
GO
ALTER TABLE [dbo].[SOLICITUD]  WITH CHECK ADD  CONSTRAINT [FK_SOLICITUD_TIPOSOLICITUD] FOREIGN KEY([TIPOSOLICITUD_ID])
REFERENCES [dbo].[TIPOSOLICITUD] ([ID_TIPOSOLICITUD])
GO
ALTER TABLE [dbo].[SOLICITUD] CHECK CONSTRAINT [FK_SOLICITUD_TIPOSOLICITUD]
GO

CREATE PROCEDURE [dbo].[ConsultarEmpleado]
@ID_EMPLEADO BIGINT
AS
BEGIN

	SELECT E.[IDENTIFICACION],E.[NOMBRECOMPLETO],E.[FECHA_NACIMIENTO]
		  ,E.[FECHA_INGRESO],E.[FOTO],E.[SALARIO],E.[SALDO_VACACIONES]
		  ,CA.NOMBRE_CARGO,C.CORREO,E.[HORARIOLABORAL_ID],D.NOMBRE_DEPARTAMENTO
	  FROM [dbo].[EMPLEADO] E
	  INNER JOIN CORREO C ON C.ID_CORREO = E.CORREO_ID
	  INNER JOIN DEPARTAMENTO D ON E.DEPARTAMENTO_ID = D.ID_DEPARTAMENTO
	  INNER JOIN CARGO CA ON E.CARGO_ID = CA.ID_CARGO
	  WHERE E.[ID_EMPLEADO] = @ID_EMPLEADO
END
GO

CREATE procedure [dbo].[ObtenerInformacionEmpleado]
as begin
SELECT 
    em.IDENTIFICACION,
    em.NOMBRECOMPLETO,
    em.FECHA_NACIMIENTO,
    em.FOTO,
    co.CORREO,
    roles.Name as ROL,
    de.DESCRIPCION AS DEPARTAMENTO,
    ca.NOMBRE_CARGO AS CARGO,
    ho.DESCRIPCION AS HORARIO,
    em.SALARIO,
    em.SALDO_VACACIONES,
    STRING_AGG(direc.DIRRECION, ', ') AS DIRRECIONES,
	STRING_AGG(tel.TELEFONO, ', ') AS TELEFONOS
FROM 
    EMPLEADO em
LEFT JOIN 
    CARGO ca ON em.CARGO_ID = ca.ID_CARGO
INNER JOIN 
    CORREO co ON co.ID_CORREO = em.CORREO_ID
LEFT JOIN  
    HORARIOLABORAL ho ON ho.ID_HORARIOLABORAL = em.HORARIOLABORAL_ID
LEFT JOIN 
    DEPARTAMENTO de ON de.ID_DEPARTAMENTO = em.DEPARTAMENTO_ID
INNER JOIN 
    AspNetUsers users ON users.Email = co.CORREO
INNER JOIN 
    AspNetUserRoles userol ON userol.UserId = users.Id
INNER JOIN 
    AspNetRoles roles ON roles.Id = userol.RoleId
LEFT JOIN 
    EMPLEADODIRRECCION empledirec ON empledirec.EMPLEADO_ID = em.ID_EMPLEADO
LEFT JOIN 
    DIRRECCION direc ON direc.ID_DIRECCION = empledirec.DIRRECION_ID
 
	LEFT JOIN EMPLEADOTELEFONO empletel on empletel.EMPLEADO_ID=em.ID_EMPLEADO
 
	LEFT join TELEFONO tel on tel.ID_TELEFONO=empletel.TELEFONO_ID
GROUP BY 
    em.IDENTIFICACION,
    em.NOMBRECOMPLETO,
    em.FECHA_NACIMIENTO,
    em.FOTO,
    co.CORREO,
    roles.Name,
    de.DESCRIPCION,
    ca.NOMBRE_CARGO,
    ho.DESCRIPCION,
    em.SALARIO,
    em.SALDO_VACACIONES
	END
GO
USE [master]
GO
ALTER DATABASE [GestionPersonalProinsa] SET  READ_WRITE 
GO
