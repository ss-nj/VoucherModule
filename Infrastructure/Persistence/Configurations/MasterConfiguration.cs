using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{
    public class MasterConfiguration : IEntityTypeConfiguration<Master>
    {
        public void Configure(EntityTypeBuilder<Master> builder)
        {
            builder.ToTable("Masters");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).IsRequired().HasMaxLength(200);
            builder.Property(m => m.Code).IsRequired().HasMaxLength(50).IsUnicode(false);
            builder.HasIndex(m => m.Code).IsUnique();
            builder.Property(m => m.CreatedAt).IsRequired();

            builder.HasOne(m => m.Owner)
                .WithMany(p => p.MastersCreated)
                .HasForeignKey(m => m.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
