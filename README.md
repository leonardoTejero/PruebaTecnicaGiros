Script para generar base de datos

USE [master]

GO

CREATE DATABASE [BdPaises]

GO

USE [BdPaises]

GO


CREATE TABLE [dbo].[PAISES] (
    [id_pais] INT IDENTITY(1,1) NOT NULL,
    [nombre] VARCHAR(100) NOT NULL,
    CONSTRAINT [PK_PAISES] PRIMARY KEY CLUSTERED ([id_pais] ASC)
);

GO


CREATE TABLE [dbo].[CIUDADES] (
    [id_ciudad] INT IDENTITY(1,1) NOT NULL,
    [nombre] VARCHAR(100) NOT NULL,
    [id_pais] INT NOT NULL,
    CONSTRAINT [PK_CIUDADES] PRIMARY KEY CLUSTERED ([id_ciudad] ASC),
    CONSTRAINT [FK_CIUDADES_PAISES] FOREIGN KEY ([id_pais]) REFERENCES [dbo].[PAISES] ([id_pais])
);

GO


CREATE TABLE [dbo].[GIROS] (
    [GIR_GIRO_ID] INT IDENTITY(1,1) NOT NULL,
    [GIR_RECIBO] VARCHAR(50) NOT NULL,
    [GIR_CIUDAD_ID] INT NOT NULL,
    CONSTRAINT [PK_GIROS] PRIMARY KEY CLUSTERED ([GIR_GIRO_ID] ASC),
    CONSTRAINT [FK_GIROS_CIUDADES] FOREIGN KEY ([GIR_CIUDAD_ID]) REFERENCES [dbo].[CIUDADES] ([id_ciudad])
);

GO

-- Crear procedimiento almacenado

create proc [dbo].[sp_obtenerPaises]
as
	select * from Paises
 
GO

create proc [dbo].[sp_guardarPais]
@nombre varchar(100)
as
	insert into PAISES values(@nombre)
 
GO
