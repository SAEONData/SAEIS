CREATE TABLE [dbo].[Literature] (
    [LiteratureID]        INT            NOT NULL,
    [Type]                NVARCHAR (MAX) NULL,
    [SubType]             NVARCHAR (MAX) NULL,
    [Title]               NVARCHAR (MAX) NULL,
    [Author]              NVARCHAR (MAX) NULL,
    [PublishDate]         NVARCHAR (MAX) NULL,
    [JournalName]         NVARCHAR (MAX) NULL,
    [JournalVolumePageNo] NVARCHAR (MAX) NULL,
    [LiteratureFocus]     NVARCHAR (MAX) NULL,
    [Available]           NVARCHAR (1)   NULL,
    [LinkToManuscript]    NVARCHAR (MAX) NULL,
    [CallNo]              NVARCHAR (MAX) NULL,
    [BarCode]             NVARCHAR (MAX) NULL,
    [Location]            NVARCHAR (MAX) NULL,
    [University]          NVARCHAR (MAX) NULL,
    [Degree]              NVARCHAR (MAX) NULL,
    [ReportNo]            NVARCHAR (MAX) NULL
);

