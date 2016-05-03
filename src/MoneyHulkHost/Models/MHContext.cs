using Microsoft.Data.Entity;
using Microsoft.Extensions.PlatformAbstractions;

namespace MoneyHulkHost.Models
{
    public class MHContext : DbContext
    {

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<AccountLine> AccountEntries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity(typeof(Category)).HasOne(typeof(Budget)).WithOne();
            modelBuilder.
                Entity(typeof(Category)).HasIndex("Name").IsUnique(true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var appEnv = (IApplicationEnvironment) CallContextServiceLocator.Locator.ServiceProvider.GetService(typeof(IApplicationEnvironment));
            optionsBuilder.UseSqlite($"Data Source={appEnv.ApplicationBasePath}/mh.sqlite");
        }
    }
}
