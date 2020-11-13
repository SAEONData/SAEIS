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
            modelBuilder.Entity<Estuary>().Property(p => p.Latitude).HasPrecision(9, 6);
            modelBuilder.Entity<Estuary>().Property(p => p.Longitude).HasPrecision(9, 6);
            modelBuilder.Entity<Estuary>().Property(p => p.AreaFloodplain).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.AreaWater).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.Distance).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.LengthShoreLine).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.BiodiversityImportance).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.ScoreImportance).HasPrecision(9, 3);
            modelBuilder.Entity<Estuary>().Property(p => p.ImportanceScoreRDM).HasPrecision(9, 3);

            // Many to Many
            modelBuilder.Entity<Estuary>()
                .HasMany(e => e.Datasets)
                .WithMany(e => e.Estuaries)
                .UsingEntity(j =>
                {
                    j.Property<int>("EstuaryID").IsRequired();
                    j.Property<int>("DatasetID").IsRequired();
                    j.ToTable("EstuaryDataset");
                });
            modelBuilder.Entity<Estuary>()
                .HasMany(e => e.Images)
                .WithMany(e => e.Estuaries)
                .UsingEntity(j =>
                {
                    j.Property<int>("EstuaryID").IsRequired();
                    j.Property<int>("ImageID").IsRequired();
                    j.ToTable("EstuaryImage");
                });

            modelBuilder.Entity<Estuary>()
                .HasMany(e => e.Literatures)
                .WithMany(e => e.Estuaries)
                .UsingEntity(j =>
                {
                    j.Property<int>("EstuaryID").IsRequired();
                    j.Property<int>("LiteratureID").IsRequired();
                    j.ToTable("EstuaryLiterature");
                });
            modelBuilder.Entity<Estuary>()
                .HasMany(e => e.Maps)
                .WithMany(e => e.Estuaries)
                .UsingEntity(j =>
                {
                    j.Property<int>("EstuaryID").IsRequired();
                    j.Property<int>("MapID").IsRequired();
                    j.ToTable("EstuaryMap");
                });
        }
    }
}
