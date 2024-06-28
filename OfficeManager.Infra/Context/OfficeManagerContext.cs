using Microsoft.EntityFrameworkCore;
using OfficeManager.Infra.Mappings;
using System.Reflection;

namespace OfficeManager.Infra.Context
{
    public class OfficeManagerContext : DbContext
    {
        public OfficeManagerContext()
        {
        }

        public OfficeManagerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FeeMapping());
            modelBuilder.ApplyConfiguration(new CaseMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new OfficeMapping());
            modelBuilder.ApplyConfiguration(new ClientMapping());
            modelBuilder.ApplyConfiguration(new DocumentMapping());
            modelBuilder.ApplyConfiguration(new ClientMeetingMapping());

            base.OnModelCreating(modelBuilder);

            Assembly assemblyWithConfigurations = GetType().Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assemblyWithConfigurations);
        }
    }
}