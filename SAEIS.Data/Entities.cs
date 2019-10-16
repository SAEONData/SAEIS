using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAEIS.Data
{

    public static class Helpers
    {
        public static string YesNo(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }
            else if (value.Equals("Y", StringComparison.CurrentCultureIgnoreCase))
            {
                return "Yes";
            }
            else if (value.Equals("N", StringComparison.CurrentCultureIgnoreCase))
            {
                return "No";
            }
            else
            {
                return null;
            }
        }
    }

    [Table("Classification")]
    public class Classification
    {
        [Required, Column("ClassificationID")]
        public int Id { get; set; }

        [Column("ClassificationType"), DisplayName("Classification")]
        public string Type { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Condition")]
    public class Condition
    {
        [Required, Column("ConditionID")]
        public int Id { get; set; }

        [Column("ConditionType"), DisplayName("Condition")]
        public string Type { get; set; }
        [DisplayName("Short description")]
        public string ShortDescription { get; set; }
        [DisplayName("Long description")]
        public string LongDescription { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Dataset")]
    public class Dataset
    {
        [Required, Column("DatasetID")]
        public int Id { get; set; }
        [Column("DatasetName")]
        public string Name { get; set; }
        public string Date { get; set; }

        // Navigation
        public List<DatasetVariable> DatasetVariables { get; set; }
    }

    [Table("DatasetVariable")]
    public class DatasetVariable
    {
        [Required, Column("DatasetVariableID")]
        public int Id { get; set; }
        [Required]
        public int DatasetId { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string VariableType { get; set; }
        public string VariableName { get; set; }
        [Column("LinkToDocument")]
        public string Link { get; set; }

        // Navigation
        public Dataset Dataset { get; set; }
    }

    [Table("Estuary")]
    public class Estuary
    {
        [Required, Column("EstuaryID")]
        public int Id { get; set; }

        [Column("EstuaryName"), DisplayName("Estuary Name")]
        public string Name { get; set; }
        [DisplayName("Alternative Name")]
        public string AlternativeName { get; set; }
        public string Place { get; set; }

        public int? ClassificationId { get; set; }
        [DisplayName("Classification date")]
        public string ClassificationDate { get; set; }
        public int? ConditionId { get; set; }
        [DisplayName("Assessment date")]
        public string ConditionDate { get; set; }
        [DisplayName("Remarks")]
        public string ConditionRemarks { get; set; }
        public int? ProvinceId { get; set; }
        [Column("BioRegionID")]
        public int? RegionId { get; set; }
        public int? GeomorphologyId { get; set; }
        public int? InfoAvailableId { get; set; }
        public int? WaterQualityId { get; set; }
        public int? ManagementClassificationId { get; set; }
        [Column("SancutaryProtectionID")]
        public int? SanctuaryProtectionId { get; set; }
        public int? UndevelopedMarginId { get; set; }
        public int? WaterRequirementId { get; set; }
        public int? PriorityForRehabilitationId { get; set; }

        [Column("LatitudeDecimalDegrees"), DisplayName("Latitude (E)")]
        public decimal? Latitude { get; set; }
        [Column("LongitudeDecimalDegrees"), DisplayName("Longitude (S)")]
        public decimal? Longitude { get; set; }

        [DisplayName("Mouth is open")]
        public int? MouthOpen { get; set; }

        [DisplayName("Area of the floodplain")]
        public decimal? AreaFloodplain { get; set; }

        [DisplayName("Area of the water surface")]
        public decimal? AreaWater { get; set; }

        [DisplayName("Length of waterline")]
        public decimal? LengthShoreLine { get; set; }

        [DisplayName("Length of river")]
        public int? LengthRiver { get; set; }

        [DisplayName("Catchment area")]
        public int? AreaCatchment { get; set; }

        [DisplayName("Catchment area DEAT")]
        public int? AreaCatchmentDEAT { get; set; }

        [Column("MARunnoff"), DisplayName("Mean annual runoff")]
        public int? MeanAnnualRunoff { get; set; }

        [Column("MinFlowReq"), DisplayName("Min flow requirement")]
        public int? MinFlowRequirement { get; set; }

        [DisplayName("Agriculture")]
        public int? PercentAgriculture { get; set; }
        [DisplayName("Degraded")]
        public int? PercentDegraded { get; set; }
        [DisplayName("Natural")]
        public int? PercentNatural { get; set; }
        [Column("PecentUrban"), DisplayName("Urban")]
        public int? PercentUrban { get; set; }
        [DisplayName("Landcover reference")]
        public string LandCoverReference { get; set; }
        [Column("SizeConsImp"), DisplayName("Size")]
        public int? SizeImportance { get; set; }
        [Column("ZonalRarityConsImp"), DisplayName("Zone type rarity")]
        public int? ZoneTypeRarity { get; set; }
        [Column("HabitatConsImp"), DisplayName("Habitat importance")]
        public int? HabitatImportance { get; set; }
        [Column("BiodiversityConsImp"), DisplayName("Biodiversity importance")]
        public decimal? BiodiversityImportance { get; set; }
        [Column("ScoreConsImp"), DisplayName("Overall score")]
        public decimal? ScoreImportance { get; set; }
        [Column("RankConsImp"), DisplayName("National rank")]
        public int? RankImportance { get; set; }
        [Column("DemandScoreRDM"), DisplayName("Demand score")]
        public int? DemandScoreRDM { get; set; }
        [Column("ImpScoreRDM"), DisplayName("Importance score")]
        public decimal? ImportanceScoreRDM { get; set; }
        [Column("PriorityRDM"), DisplayName("National priority")]
        public int? NationalPriorityRDM { get; set; }
        [Column("DemandCatRDM"), DisplayName("Demand category")]
        public string DemandCategoryRDM { get; set; }
        [Column("ImpCatRDM"), DisplayName("Importance category")]
        public string ImportanceCategoryRDM { get; set; }
        [Column("ProvincialConsImp"), DisplayName("Provincial conservation importance")]
        public string ProvincialImportance { get; set; }
        [Column("NationalConsImp"), DisplayName("National conservation importance")]
        public string NationalImportance { get; set; }
        [Column("BiblioWitfieldLink"), DisplayName("Bibliography Whitfield link")]
        public string WhitfieldLink { get; set; }
        [Column("WaterQualityPollution")]
        public string WaterQualityPollutionYN { get; set; }
        [DisplayName("Water quality (pollution)"), NotMapped]
        public string WaterQualityPollution { get => Helpers.YesNo(WaterQualityPollutionYN); }
        [Column("WaterQualitySilt")]
        public string WaterQualitySiltYN { get; set; }
        [DisplayName("Water quality (silt)"), NotMapped]
        public string WaterQualitySilt { get => Helpers.YesNo(WaterQualitySiltYN); }
        [Column("WaterQuantityGen"), DisplayName("Water quantity")]
        public string WaterQuantityYN { get; set; }
        [DisplayName("Water quantity"), NotMapped]
        public string WaterQuantity { get => Helpers.YesNo(WaterQuantityYN); }
        [Column("AlienClearing")]
        public string AlienClearingYN { get; set; }
        [DisplayName("Alien clearing"), NotMapped]
        public string AlienClearing { get => Helpers.YesNo(AlienClearingYN); }
        [Column("FixAppropriateBankStabilisation")]
        public string FixAppropriateBankStabilisationYN { get; set; }
        [DisplayName("Fix appropriate bank stabilisation"), NotMapped]
        public string FixAppropriateBankStabilisation { get => Helpers.YesNo(FixAppropriateBankStabilisationYN); }
        [Column("MouthManagement")]
        public string MouthManagementYN { get; set; }
        [DisplayName("Mouth management"), NotMapped]
        public string MouthManagement { get => Helpers.YesNo(MouthManagementYN); }
        [DisplayName("Rehabilitation comments")]
        public string RehabilitationComments { get; set; }
        [DisplayName("Plan"), NotMapped]
        public string ManagementPlan { get; set; }
        [DisplayName("Status"), NotMapped]
        public string ManagementPlanStatus { get; set; }
        [DisplayName("Link"), NotMapped]
        public string ManagementPlanLink { get; set; }

        // Navigation
        public Classification Classification { get; set; }
        public Condition Condition { get; set; }
        public Geomorphology Geomorphology { get; set; }
        public InfoAvailable InfoAvailable { get; set; }
        public ManagementClassification ManagementClassification { get; set; }
        public PriorityForRehabilitation PriorityForRehabilitation { get; set; }
        public Province Province { get; set; }
        public Region Region { get; set; }
        public SanctuaryProtection SanctuaryProtection { get; set; }
        public UndevelopedMargin UndevelopedMargin { get; set; }
        public WaterQuality WaterQuality { get; set; }
        public WaterRequirement WaterRequirement { get; set; }
        public List<Issue> Issues { get; set; }
        public List<EstuaryLiterature> EstuaryLiteratures { get; set; }
        public List<EstuaryMap> EstuaryMaps { get; set; }
        public List<EstuaryImage> EstuaryImages { get; set; }
        public List<EstuaryDataset> EstuaryDatasets { get; set; }
    }

    [Table("EstuaryDataset")]
    public class EstuaryDataset
    {
        [Required]
        public int EstuaryId { get; set; }
        [Required]
        public int DatasetId { get; set; }

        // Navigation
        public Estuary Estuary { get; set; }
        public Dataset Dataset { get; set; }
    }

    [Table("EstuaryLiterature")]
    public class EstuaryLiterature
    {
        [Required]
        public int EstuaryId { get; set; }
        [Required]
        public int LiteratureId { get; set; }

        // Navigation
        public Estuary Estuary { get; set; }
        public Literature Literature { get; set; }
    }

    [Table("EstuaryMap")]
    public class EstuaryMap
    {
        [Required]
        public int EstuaryId { get; set; }
        [Required]
        public int MapId { get; set; }

        // Navigation
        public Estuary Estuary { get; set; }
        public Map Map { get; set; }
    }

    [Table("EstuaryImage")]
    public class EstuaryImage
    {
        [Required]
        public int EstuaryId { get; set; }
        [Required]
        public int ImageId { get; set; }

        // Navigation
        public Estuary Estuary { get; set; }
        public Image Image { get; set; }
    }

    [Table("Geomorphology")]
    public class Geomorphology
    {
        [Required, Column("GeomorphologyID")]
        public int Id { get; set; }

        [DisplayName("Geomorphology")]
        public string Category { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Images")]
    public class Image
    {
        [Required, Column("ImageID")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Name { get; set; }
        [Column("Available")]
        public string AvailableYN { get; set; }
        [NotMapped]
        public string Available { get => Helpers.YesNo(AvailableYN); }
        [Column("ImageDate")]
        public string Date { get; set; }
        [Column("LinkToImage")]
        public string Link { get; set; }
        public string Source { get; set; }
        public string Reference { get; set; }
        public string Notes { get; set; }
    }

    [Table("InfoAvailable")]
    public class InfoAvailable
    {
        [Required, Column("InfoAvailableID")]
        public int Id { get; set; }

        [DisplayName("Information available")]
        public string Category { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Issue")]
    public class Issue
    {
        [Required, Column("IssueID")]
        public int Id { get; set; }
        [Required]
        public int EstuaryId { get; set; }
        [Required]
        public int IssueTypeId { get; set; }
        [Column("IssueHeader")]
        public string Header { get; set; }
        public string Notes { get; set; }
        public string Responses { get; set; }

        // Navigation
        public Estuary Estuary { get; set; }
        public IssueType IssueType { get; set; }
    }

    [Table("IssueType")]
    public class IssueType
    {
        [Required, Column("IssueTypeID")]
        public int Id { get; set; }
        [Column("IssueType"), DisplayName("Issue type")]
        public string Type { get; set; }

        // Navigation
        public List<Issue> Issues { get; set; }
    }

    [Table("Literature")]
    public class Literature
    {
        [Required, Column("LiteratureID")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        public string Author { get; set; }
        [Column("PublishDate")]
        public string Year { get; set; }
        public string Title { get; set; }
        [Column("Available")]
        public string AvailableYN { get; set; }
        [NotMapped]
        public string Available { get => Helpers.YesNo(AvailableYN); }
        [Column("LinkToManuscript")]
        public string Link { get; set; }

        // Navigation
        public List<EstuaryLiterature> EstuaryLiteratures { get; set; }

    }

    [Table("ManagementClassification")]
    public class ManagementClassification
    {
        [Required, Column("ManagementClassificationID")]
        public int Id { get; set; }

        [Column("ClassificationType"), DisplayName("Management classification")]
        public string Type { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Map")]
    public class Map
    {
        [Required, Column("MapID")]
        public int Id { get; set; }
        public string Type { get; set; }
        public string SubType { get; set; }
        [Column("MapName")]
        public string Name { get; set; }
        [Column("Available")]
        public string AvailableYN { get; set; }
        [NotMapped]
        public string Available { get => Helpers.YesNo(AvailableYN); }
        [Column("LinkToMap")]
        public string Link { get; set; }
    }

    [Table("PriorityForRehabilitation")]
    public class PriorityForRehabilitation
    {
        [Required, Column("PriorityForRehabilitationID")]
        public int Id { get; set; }

        [DisplayName("Priority for rehabilitation")]
        public string Priority { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Province")]
    public class Province
    {
        [Required, Column("ProvinceID")]
        public int Id { get; set; }

        public string Code { get; set; }
        public string Name { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("BioRegion")]
    public class Region
    {
        [Required, Column("BioRegionID")]
        public int Id { get; set; }

        [DisplayName("BioRegion")]
        public string Category { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("SancutaryProtection")]
    public class SanctuaryProtection
    {
        [Required, Column("SancutaryProtectionID")]
        public int Id { get; set; }

        [DisplayName("Recommended extent of sanctuary protection")]
        public string RecommendedExtent { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("UndevelopedMargin")]
    public class UndevelopedMargin
    {
        [Required, Column("UndevelopedMarginID")]
        public int Id { get; set; }

        [DisplayName("Recommended extent of undeveloped margin")]
        public string RecommendedExtent { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("WaterQuality")]
    public class WaterQuality
    {
        [Required, Column("WaterQualityID")]
        public int Id { get; set; }

        [DisplayName("Water quality")]
        public string Category { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("WaterRequirement")]
    public class WaterRequirement
    {
        [Required, Column("WaterRequirementID")]
        public int Id { get; set; }

        [DisplayName("Recommended minimum water requirement")]
        public string RecommendedMinimum { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }


    public class SAEISDbContext : DbContext
    {
        public SAEISDbContext(DbContextOptions<SAEISDbContext> options) : base(options)
        {
        }

        public DbSet<Classification> Classifications { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Dataset> Datasets { get; set; }
        public DbSet<DatasetVariable> DatasetVariables { get; set; }
        public DbSet<Estuary> Estuaries { get; set; }
        public DbSet<Geomorphology> Geomorphologies { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueType> IssueTypes { get; set; }
        public DbSet<Literature> Literatures { get; set; }
        public DbSet<ManagementClassification> ManagementClassifications { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<PriorityForRehabilitation> PriorityForRehabilitations { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SanctuaryProtection> SanctuaryProtections { get; set; }
        public DbSet<UndevelopedMargin> UndevelopedMargins { get; set; }
        public DbSet<WaterQuality> WaterQualities { get; set; }
        public DbSet<WaterRequirement> WaterRequirements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EstuaryLiterature>()
                .HasKey(el => new { el.EstuaryId, el.LiteratureId });
            modelBuilder.Entity<EstuaryMap>()
                .HasKey(el => new { el.EstuaryId, el.MapId });
            modelBuilder.Entity<EstuaryImage>()
                .HasKey(el => new { el.EstuaryId, el.ImageId });
            modelBuilder.Entity<EstuaryDataset>()
                .HasKey(el => new { el.EstuaryId, el.DatasetId });
        }
    }
}