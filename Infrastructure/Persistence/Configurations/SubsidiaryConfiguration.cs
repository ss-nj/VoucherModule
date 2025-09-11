using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{

    public class SubsidiaryConfiguration : IEntityTypeConfiguration<Subsidiary>
    {
        public void Configure(EntityTypeBuilder<Subsidiary> builder)
        {
            builder.ToTable("Subsidiaries");
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Title).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Code).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.DebitAmount).HasDefaultValue(0);
            builder.Property(s => s.CreditAmount).HasDefaultValue(0);
            builder.Property(s => s.IsLastLevel).IsRequired();
            builder.Property(s => s.IsDeleted).IsRequired().HasDefaultValue(false);
            builder.Property(s => s.CreatedAt).IsRequired();
            builder.HasOne(s => s.Owner)
                .WithMany(p => p.SubsidiariesCreated)
                .HasForeignKey(s => s.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Master)
                .WithMany(m => m.Subsidiaries)
                .HasForeignKey(s => s.MasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(s => s.ParentSubsidiary)
                .WithMany(p => p.ChildSubsidiaries)
                .HasForeignKey(s => s.ParentSubsidiaryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
        }
    }


}
