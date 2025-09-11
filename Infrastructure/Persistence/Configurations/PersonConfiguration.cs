using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
{

    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.NationalId).IsRequired().HasMaxLength(10).IsUnicode(false);
            builder.HasIndex(p => p.NationalId).IsUnique();
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20).IsUnicode(false);
            builder.HasIndex(p => p.PhoneNumber).IsUnique();
            builder.Property(p => p.CreatedAt).IsRequired();

        }
    }

}
