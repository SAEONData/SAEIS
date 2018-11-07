CREATE TABLE [dbo].[Survey] (
    [SurveyID]      INT            NOT NULL,
    [EstuaryID]     INT            NOT NULL,
    [SurveyType]    NVARCHAR (MAX) NULL,
    [SurveySubType] NVARCHAR (MAX) NULL,
    [SurveyDate]    NVARCHAR (MAX) NULL,
    [Reference]     NVARCHAR (MAX) NULL,
    [Notes]         NVARCHAR (MAX) NULL
);

