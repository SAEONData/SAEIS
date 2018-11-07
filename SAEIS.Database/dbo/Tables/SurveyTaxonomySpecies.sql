CREATE TABLE [dbo].[SurveyTaxonomySpecies] (
    [SurveyID]          INT            NOT NULL,
    [TaxonomySpeciesID] INT            NOT NULL,
    [AbundanceMeasure]  INT            NULL,
    [Abundance]         NVARCHAR (MAX) NULL,
    [Notes]             NVARCHAR (MAX) NULL
);

