CREATE TABLE [dbo].[DatasetVariable] (
    [DatasetVariableID] INT            NOT NULL,
    [DatasetID]         INT            NULL,
    [Type]              NVARCHAR (MAX) NULL,
    [SubType]           NVARCHAR (MAX) NULL,
    [VariableType]      NVARCHAR (MAX) NULL,
    [VariableName]      NVARCHAR (MAX) NULL,
    [Available]         NVARCHAR (1)   NULL,
    [LinkToDocument]    NVARCHAR (MAX) NULL
);

