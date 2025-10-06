using Dal.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal;

/// <summary>
/// Контекст базы данных для сервиса GiftCatalog
/// </summary>
public class GiftCatalogDbContext : DbContext
{
    /// <summary>
    /// Конструктор контекста базы данных
    /// </summary>
    public GiftCatalogDbContext(DbContextOptions<GiftCatalogDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Таблица вендоров
    /// </summary>
    public DbSet<VendorDal> Vendors { get; set; } = null!;

    /// <summary>
    /// Таблица номиналов
    /// </summary>
    public DbSet<DenominationDal> Denominations { get; set; } = null!;

    /// <summary>
    /// Таблица партий подарочных карт
    /// </summary>
    public DbSet<GiftBatchDal> Batches { get; set; } = null!;

    /// <summary>
    /// Таблица подарочных карт
    /// </summary>
    public DbSet<GiftCardDal> Cards { get; set; } = null!;

    /// <summary>
    /// Настройка сущностей и связей между ними.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // VendorDal
        modelBuilder.Entity<VendorDal>(entity =>
        {
            entity.ToTable(nameof(VendorDal));
            entity.HasKey(x => x.Id);
            entity.HasIndex(x => x.Slug).IsUnique();
            entity.Property(x => x.Name).HasMaxLength(128).IsRequired();
            entity.Property(x => x.Slug).HasMaxLength(64).IsRequired();
        });

        // DenominationDal
        modelBuilder.Entity<DenominationDal>(entity =>
        {
            entity.ToTable(nameof(DenominationDal));
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Currency).HasMaxLength(3).IsRequired();
            entity.HasIndex(x => new { x.Amount, x.Currency }).IsUnique();
        });

        // GiftBatchDal
        modelBuilder.Entity<GiftBatchDal>(entity =>
        {
            entity.ToTable(nameof(GiftBatchDal));
            entity.HasKey(x => x.Id);

            entity.HasOne<VendorDal>()
                .WithMany()
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<DenominationDal>()
                .WithMany()
                .HasForeignKey(x => x.DenominationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(x => x.Status)
                .HasConversion<string>() // Enum как строка
                .HasMaxLength(32)
                .IsRequired();
        });

        // GiftCardDal
        modelBuilder.Entity<GiftCardDal>(entity =>
        {
            entity.ToTable(nameof(GiftCardDal));
            entity.HasKey(x => x.Id);

            entity.HasOne<GiftBatchDal>()
                .WithMany()
                .HasForeignKey(x => x.BatchId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(x => x.CodeHash).HasMaxLength(64).IsRequired();
            entity.HasIndex(x => x.CodeHash).IsUnique();

            entity.Property(x => x.Status)
                .HasConversion<string>() // Enum как строка
                .HasMaxLength(32)
                .IsRequired();
        });
    }
}
