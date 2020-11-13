CREATE TABLE [dbo].[EstuaryMap] (
    [EstuaryMapID] INT NOT NULL Identity,
    [EstuaryID]    INT NOT NULL,
    [MapID]        INT NOT NULL,
    CONSTRAINT [FK_EstuaryMap_EstuaryID] FOREIGN KEY (EstuaryID) REFERENCES [Estuary]([EstuaryID]),
    CONSTRAINT [FK_EstuaryMap_MapID] FOREIGN KEY (MapID) REFERENCES [Map]([MapID]), 
    CONSTRAINT [PK_EstuaryMap] PRIMARY KEY ([EstuaryID], [MapID])
);



GO
CREATE INDEX [IX_EstuaryMap_EstuaryID] ON [dbo].[EstuaryMap] ([EstuaryID])
GO
CREATE INDEX [IX_EstuaryMap_MapID] ON [dbo].[EstuaryMap] ([MapID])


