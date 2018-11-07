CREATE TABLE [dbo].[PressuresThreat] (
    [PressuresThreatID] INT            NOT NULL,
    [EstuaryID]         INT            NOT NULL,
    [Type]              INT            NOT NULL,
    [AssessmentMonth]   INT            NOT NULL,
    [AssessmentYear]    INT            NOT NULL,
    [Pressure]          NVARCHAR (MAX) NULL,
    [Threat]            NVARCHAR (MAX) NULL,
    [PTrend]            INT            NULL,
    [PRange]            INT            NULL,
    [PImpact]           INT            NULL,
    [PPermanance]       INT            NULL,
    [TLikelihood]       INT            NULL,
    [TRange]            INT            NULL,
    [TImpact]           INT            NULL,
    [TPermanance]       INT            NULL,
    [ReferenceID]       INT            NULL
);

