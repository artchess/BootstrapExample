CREATE TABLE [dbo].[Autor] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [PrimerNombre]  NVARCHAR (200)  NULL,
    [SegundoNombre] NVARCHAR (200)  NULL,
    [Biografia]     NVARCHAR (2000) NULL,
    CONSTRAINT [PK_Autor] PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Libro](
	[Id] INT IDENTITY (1, 1) NOT NULL,
	[AutorId] INT NOT NULL,
	[Titulo] NVARCHAR(200) NULL,
	[Isbn] NVARCHAR(200) NULL,
	[Sinopsis] NVARCHAR(200) NULL,
	[Descripcion] NVARCHAR(2000) NULL,
	[UrlImagen] NVARCHAR(200) NULL,
	CONSTRAINT [PK_Libro] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Libro_Autor] FOREIGN KEY ([AutorId])
		REFERENCES [dbo].[Autor] ([Id]) ON DELETE CASCADE
);