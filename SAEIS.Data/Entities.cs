using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAEIS.Data
{
    [Table("Classification")]
    public class Classification
    {
        [Required, Column("ClassificationID")]
        public int Id { get; set; }

        [Column("ClassificationType")]
        public string Type { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Condition")]
    public class Condition
    {
        [Required, Column("ConditionID")]
        public int Id { get; set; }

        [Column("ConditionType")]
        public string Type { get; set; }

        // Navigation
        public List<Estuary> Estuaries { get; set; }
    }

    [Table("Estuary")]
    public class Estuary
    {
        [Required, Column("EstuaryID")]
        public int Id { get; set; }

        [Column("EstuaryName")]
        public string Name { get; set; }

        public int ClassificationId { get; set; }
        public int ConditionId { get; set; }
        public int ProvinceId { get; set; }
        [Column("BioRegionID")]
        public int RegionId { get; set; }

        [Column("LatitudeDecimalDegrees")]
        public decimal? Latitude { get; set; }
        [Column("LongitudeDecimalDegrees")]
        public decimal? Longitude { get; set; }

        // Navigation
        public Classification Classification { get; set; }

        public Condition Condition { get; set; }
        public Province Province { get; set; }
        public Region Region { get; set; }
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

        public string Category { get; set; }

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
        public DbSet<Estuary> Estuaries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Region> Regions { get; set; }
    }
}