CREATE TABLE [dbo].[Images] (
    [ImageID]     INT            NOT NULL,
    [Type]        NVARCHAR (MAX) NULL,
    [SubType]     NVARCHAR (MAX) NULL,
    [Name]        NVARCHAR (MAX) NULL,
    [Code]        NVARCHAR (MAX) NULL,
    [ImageDate]   NVARCHAR (MAX) NULL,
    [LinkToImage] NVARCHAR (MAX) NULL,
    [Available]   NVARCHAR (1)   NULL,
    [Source]      NVARCHAR (MAX) NULL,
    [Reference]   NVARCHAR (MAX) NULL,
    [Notes]       NVARCHAR (MAX) NULL
);

