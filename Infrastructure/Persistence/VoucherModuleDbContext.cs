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

            // calc path for added/modified Subsidiaries
            foreach (var entry in ChangeTracker.Entries<Subsidiary>()
                         .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
            {
                var s = entry.Entity;
                var parentPath = await GetParentPathAsync(s, cancellationToken);

                // If new 
                if (entry.State == EntityState.Added && s.Id == 0)
                {
                    entry.Property(x => x.ParentPath).CurrentValue = parentPath;
                }
                else
                {
                    s.ParentPath = $"{parentPath}{s.Id}-";
                }
            }

            var result = await base.SaveChangesAsync(cancellationToken);

            // ParentPath for new Subsidiaries
            var newSubsidiaries = ChangeTracker.Entries<Subsidiary>()
                .Where(e => e.State == EntityState.Added && e.Entity.Id > 0)
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
