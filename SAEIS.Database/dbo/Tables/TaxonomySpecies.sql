CREATE TABLE [dbo].[TaxonomySpecies] (
    [TaxonomySpeciesID] INT            NOT NULL,
    [TaxonomyGenusID]   INT            NULL,
    [Species]           NVARCHAR (MAX) NULL,
    [Subspecies]        NVARCHAR (MAX) NULL,
    [Variety]           NVARCHAR (MAX) NULL,
    [CommonName]        NVARCHAR (MAX) NULL,
    [SEASpp]            SMALLINT       NULL,
    [RedData]           INT            NULL,
    [EndemicRSA]        SMALLINT       NULL,
    [EndemicKZN]        SMALLINT       NULL,
    [EndemicKZNNear]    SMALLINT       NULL,
    [RestrictedKZN]     SMALLINT       NULL,
    [Notes]             NVARCHAR (MAX) NULL
);

