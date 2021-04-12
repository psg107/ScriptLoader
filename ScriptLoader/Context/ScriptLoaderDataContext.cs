using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using ScriptLoader.Entities;

namespace ScriptLoader.Context
{
    public class ScriptLoaderDataContext : DbContext
    {
        public DbSet<Setting> Setting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = new SqliteConnection()
            {
                ConnectionString = new SqliteConnectionStringBuilder() { DataSource = $@"|DataDirectory|\Data.sqlite" }.ConnectionString
            };

            optionsBuilder.UseSqlite(@"Data Source=Data.sqlite");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultSettings = ScriptLoaderDataContextSeed.GenerateSetting();
            modelBuilder.Entity<Setting>().HasData(defaultSettings);
        }
    }
}
