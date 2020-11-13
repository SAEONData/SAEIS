CREATE TABLE [dbo].[Dataset] (
    [DatasetID]   INT            NOT NULL Identity,
    [DatasetName] NVARCHAR (MAX) NULL,
    [Date]        NVARCHAR (MAX) NULL, 
    CONSTRAINT [PK_Dataset] PRIMARY KEY ([DatasetID])
);

