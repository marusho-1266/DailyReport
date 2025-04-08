using Microsoft.EntityFrameworkCore;
using DailyReportApp.Models;

namespace DailyReportApp
{
    public class DailyReportAppDbContext : DbContext
    {
        public DbSet<DailyReport> DailyReports { get; set; }
        public DbSet<AppSettings> AppSettings { get; set; }
        public DbSet<ReportTemplate> ReportTemplates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DailyReportApp.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DailyReport>().ToTable("DailyReports");
            modelBuilder.Entity<AppSettings>().ToTable("AppSettings");
            modelBuilder.Entity<ReportTemplate>().ToTable("ReportTemplates");
        }
    }
}
