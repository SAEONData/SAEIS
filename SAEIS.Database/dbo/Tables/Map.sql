CREATE TABLE [dbo].[Map] (
    [MapID]     INT            NOT NULL Identity,
    [Type]      NVARCHAR (MAX) NULL,
    [SubType]   NVARCHAR (MAX) NULL,
    [MapName]   NVARCHAR (MAX) NULL,
    [Available] NVARCHAR (1)   NULL,
    [LinkToMap] NVARCHAR (MAX) NULL, 
    CONSTRAINT [PK_Map] PRIMARY KEY ([MapID])
);

