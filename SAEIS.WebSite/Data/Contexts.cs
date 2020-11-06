using Microsoft.EntityFrameworkCore;

namespace SAEIS.WebSite.Data
{
    public class SAEISContext : DbContext
    {
        public SAEISContext(DbContextOptions<SAEISContext> options) : base(options)
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
            modelBuilder.Entity<Estuary>().Property(p => p.Latitude).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<Estuary>().Property(p => p.Longitude).HasColumnType("decimal(9,6)");
            modelBuilder.Entity<Estuary>().Property(p => p.AreaFloodplain).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.AreaWater).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.Distance).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.LengthShoreLine).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.BiodiversityImportance).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.ScoreImportance).HasColumnType("decimal(9,3)");
            modelBuilder.Entity<Estuary>().Property(p => p.ImportanceScoreRDM).HasColumnType("decimal(9,3)");

            // Many to Many
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
