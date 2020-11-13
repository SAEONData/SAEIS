CREATE TABLE [dbo].[EstuaryLiterature] (
    [EstuaryLiteratureID] INT NOT NULL Identity,
    [EstuaryID]           INT NOT NULL,
    [LiteratureID]        INT NOT NULL,
    CONSTRAINT [FK_EstuaryLiterature_EstuaryID] FOREIGN KEY ([EstuaryID]) REFERENCES [Estuary]([EstuaryID]),
    CONSTRAINT [FK_EstuaryLiterature_LiteratureID] FOREIGN KEY ([LiteratureID]) REFERENCES [Literature]([LiteratureID]), 
    CONSTRAINT [PK_EstuaryLiterature] PRIMARY KEY ([EstuaryID], [LiteratureID])
);

GO
CREATE INDEX [IX_EstuaryLiterature_EstuaryID] ON [dbo].[EstuaryLiterature] ([EstuaryID])
GO
CREATE INDEX [IX_EstuaryLiterature_LiteratureID] ON [dbo].[EstuaryLiterature] ([LiteratureID])
