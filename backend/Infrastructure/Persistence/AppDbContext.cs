using LeadQualifier.Domain.Entities;
using LeadQualifier.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LeadQualifier.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Lead> Leads { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Lead>(b =>
        {
            b.ToTable("Leads");

            b.HasKey(x => x.Id);

            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.UpdatedAt);

            // Simple scalar fields
            b.Property<string?>("Name").HasColumnName("Name");

            // ValueObjects -> stored as simple columns via ValueConverters
            // Email -> string
            var emailConverter = new ValueConverter<Email?, string?>(
                v => v == null ? null : v.Address,
                v => v == null ? null : new Email(v)
            );
            b.Property(e => e.Email)
                .HasConversion(emailConverter)
                .HasColumnName("Email")
                .HasMaxLength(256);

            // PhoneNumber -> string
            var phoneConverter = new ValueConverter<PhoneNumber?, string?>(
                v => v == null ? null : v.Number,
                v => v == null ? null : new PhoneNumber(v)
            );
            b.Property(e => e.Phone)
                .HasConversion(phoneConverter)
                .HasColumnName("Phone")
                .HasMaxLength(32);

            // CompanyName -> string
            var companyConverter = new ValueConverter<CompanyName?, string?>(
                v => v == null ? null : v.Name,
                v => v == null ? null : new CompanyName(v)
            );
            b.Property(e => e.Company)
                .HasConversion(companyConverter)
                .HasColumnName("Company")
                .HasMaxLength(200);

            // Budget, Need, Authority
            b.Property(x => x.Budget).HasColumnType("decimal(18,2)");
            b.Property(x => x.Need).HasColumnName("Need").HasMaxLength(1000);
            b.Property(x => x.Authority).HasColumnName("Authority").HasMaxLength(200);

            // Scoring VO mapping as two ints
            b.OwnsOne(x => x.Scoring, sb =>
            {
                sb.Property(s => s.Fit).HasColumnName("FitScore");
                sb.Property(s => s.Intent).HasColumnName("IntentScore");
            });

            // Enums are stored as ints (default)
            b.Property(x => x.Status).HasConversion<int>().HasColumnName("Status");
            b.Property(x => x.Source).HasConversion<int>().HasColumnName("Source");
            b.Property(x => x.Priority).HasConversion<int>().HasColumnName("Priority");
            b.Property(x => x.QualificationLevel).HasConversion<int>().HasColumnName("QualificationLevel");

            // Indexes
            b.HasIndex(x => x.CreatedAt);
            b.HasIndex("Email").IsUnique(false);
            b.HasIndex("Company");
        });
    }
}
