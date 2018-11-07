CREATE TABLE [dbo].[BreachHistory] (
    [BreachHistoryID] INT            NOT NULL,
    [EstuaryID]       INT            NOT NULL,
    [Month]           INT            NULL,
    [Year]            INT            NULL,
    [Status]          NVARCHAR (MAX) NULL,
    [Notes]           NVARCHAR (MAX) NULL
);

