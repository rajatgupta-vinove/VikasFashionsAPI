using Microsoft.EntityFrameworkCore;

namespace VikasFashionsAPI.Data
{
    public class DataContextVikasFashion : DbContext
    {
        public DataContextVikasFashion(DbContextOptions options) : base(options)
        {
            //options.UseSqlServer(connection, b => b.MigrationsAssembly("CoreConsoleApp"))
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Blog>()
            //     .Property(b => b.Created)
            //     .HasDefaultValueSql("getdate()");
            //modelBuilder.Entity<Accessory>()
            //.Property(b => b.UserId)
            //.HasDefaultValue(0);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer();
            //base.OnConfiguring(optionsBuilder);
        }
        public DbSet<BinLocation> BinLocations { get; set; } 
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Chart> Charts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Design> Designs { get; set; }
        public DbSet<HouseBank> HouseBanks { get; set; }
        public DbSet<HSN> HSNs { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialGroup> MaterialGroups { get; set; }
        public DbSet<MaterialType> materialTypes { get; set; }
        public DbSet<PlantBranch> PlantBranches { get; set; }
        public DbSet<PostingPeriod> postingPeriods { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Shade> Shades { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UnitsOfMeasure> UnitsOfMeasures { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<BusinessPartner> BusinessPartners { get; set; }
        public DbSet<BusinessPartnerAddress> BusinessPartnerAddresses { get; set; }
        public DbSet<BusinessPartnersBankDetail> BusinessPartnersBankDetails { get; set; }
        public DbSet<BusinessPartnerType> BusinessPartnerTypes { get; set; }
        public DbSet<CompanyGroup> CompanyGroups { get; set; }
        public DbSet<PaymentTerm> PaymentTerms { get; set; }
        public DbSet<WithHoldingTax> WithHoldingTaxes { get; set; }
    }
}
