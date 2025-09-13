using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence
{

    public class VoucherModuleDbContext : DbContext
    {
        public VoucherModuleDbContext(DbContextOptions<VoucherModuleDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_100_CI_AI_SC_UTF8");

            //modelBuilder.ApplyConfiguration(new PersonConfiguration()); // in case of dosent added automaticaly by framwork
            //modelBuilder.ApplyConfiguration(new MasterConfiguration());
            //modelBuilder.ApplyConfiguration(new SubsidiaryConfiguration());
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<Subsidiary> Subsidiaries { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var now = DateTime.UtcNow;


            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                    entry.Entity.CreatedAt = now;
            }




            foreach (var entry in ChangeTracker.Entries<Subsidiary>()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var s = entry.Entity;

                if (s.ParentSubsidiaryId.HasValue)
                {

                    var parent = s.ParentSubsidiary ?? await Subsidiaries
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == s.ParentSubsidiaryId, cancellationToken);

                    var parentPath = parent?.ParentPath ?? "-";

                    entry.Property(x => x.ParentPath).CurrentValue = $"{parentPath}{s.ParentSubsidiaryId}-";
                }
                else
                {
                    entry.Property(x => x.ParentPath).CurrentValue = "-";
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);


            var newSubsidiaries = ChangeTracker.Entries<Subsidiary>()
                .Where(e => e.State == EntityState.Unchanged && e.Entity.Id > 0 && string.IsNullOrEmpty(e.Entity.ParentPath))
                .ToList();

            if (newSubsidiaries.Any())
            {
                foreach (var entry in newSubsidiaries)
                {
                    var s = entry.Entity;
                    var parentPath = await GetParentPathAsync(s, cancellationToken);
                    s.ParentPath = $"{parentPath}{s.Id}-";


                    Entry(s).Property(x => x.ParentPath).IsModified = true;
                }

                await base.SaveChangesAsync(cancellationToken);
            }

            return result;
        }

        private async Task<string> GetParentPathAsync(Subsidiary s, CancellationToken cancellationToken = default)
        {
            if (!s.ParentSubsidiaryId.HasValue)
                return "-";

            // If parent is already loaded, use it
            var parent = s.ParentSubsidiary ?? await Subsidiaries
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == s.ParentSubsidiaryId, cancellationToken);

            return parent?.ParentPath ?? "-";
        }

    }

}
