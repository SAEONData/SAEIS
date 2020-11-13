CREATE TABLE [dbo].[EstuaryImage] (
    [EstuaryImageID] INT NOT NULL Identity, 
    [ImageID]        INT NOT NULL,
    [EstuaryID]      INT NOT NULL,
    CONSTRAINT [FK_EstuaryImage_EstuaryID] FOREIGN KEY ([EstuaryID]) REFERENCES [Estuary]([EstuaryID]),
    CONSTRAINT [FK_EstuaryImage_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Images]([ImageID]), 
    CONSTRAINT [PK_EstuaryImage] PRIMARY KEY ([EstuaryID],[ImageID])
);

GO
CREATE INDEX [IX_EstuaryImage_EstuaryID] ON [dbo].[EstuaryImage] ([EstuaryID])
GO
CREATE INDEX [IX_EstuaryImage_ImageID] ON [dbo].[EstuaryImage] ([ImageID])

