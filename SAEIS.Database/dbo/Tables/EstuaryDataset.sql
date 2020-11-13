CREATE TABLE [dbo].[EstuaryDataset] (
    [EstuaryDatasetID] INT NOT NULL Identity, 
    [EstuaryID]        INT NOT NULL,
    [DatasetID]        INT NOT NULL,
    CONSTRAINT [FK_EstuaryDataset_EstuaryID] FOREIGN KEY (EstuaryID) REFERENCES [Estuary]([EstuaryID]),
    CONSTRAINT [FK_EstuaryDataset_DatasetID] FOREIGN KEY (DatasetID) REFERENCES [Dataset]([DatasetID]), 
    CONSTRAINT [PK_EstuaryDataset] PRIMARY KEY ([EstuaryID], [DatasetID])
);


GO
CREATE INDEX [IX_EstuaryDataset_EstuaryID] ON [dbo].[EstuaryDataset] ([EstuaryID])
GO
CREATE INDEX [IX_EstuaryDataset_DatasetID] ON [dbo].[EstuaryDataset] ([DatasetID])

