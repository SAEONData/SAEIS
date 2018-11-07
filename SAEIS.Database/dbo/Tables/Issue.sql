CREATE TABLE [dbo].[Issue] (
    [IssueID]     INT            NOT NULL,
    [EstuaryID]   INT            NOT NULL,
    [IssueTypeID] INT            NOT NULL,
    [IssueHeader] NVARCHAR (MAX) NULL,
    [Notes]       NVARCHAR (MAX) NULL,
    [Responses]   NVARCHAR (MAX) NULL
);

