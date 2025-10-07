using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

/// <summary>
/// контекст базы данных активаций
/// </summary>
public class ActivationDbContext : DbContext
{
    public ActivationDbContext(DbContextOptions<ActivationDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<ActivationEntity> Activations => Set<ActivationEntity>();
    public DbSet<ActivationEventEntity> ActivationEvents => Set<ActivationEventEntity>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<UserEntity>(e =>
        {
            e.ToTable("users");
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).HasMaxLength(256).IsRequired();
            e.Property(x => x.Name).HasMaxLength(128).IsRequired();
            e.HasIndex(x => x.Email).IsUnique();
        });

        b.Entity<ActivationEntity>(e =>
        {
            e.ToTable("activations");
            e.HasKey(x => x.Id);
            e.Property(x => x.CardCodeHash).HasMaxLength(64).IsRequired();
            e.Property(x => x.IdempotencyKey).HasMaxLength(64).IsRequired();
            e.HasIndex(x => x.IdempotencyKey).IsUnique();
            e.Property(x => x.Status).HasMaxLength(32).IsRequired();
        });

        b.Entity<ActivationEventEntity>(e =>
        {
            e.ToTable("activation_events");
            e.HasKey(x => x.Id);
            e.Property(x => x.EventType).HasMaxLength(32).IsRequired();
            e.Property(x => x.Message).HasMaxLength(1024).IsRequired();
            e.HasIndex(x => x.ActivationId);
        });
    }
}