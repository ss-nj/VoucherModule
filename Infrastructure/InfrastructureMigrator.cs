using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class InfrastructureMigrator
    {
        public static async Task MigrateDatabaseAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<VoucherModuleDbContext>();
            await db.Database.MigrateAsync();
        }
    }
}
