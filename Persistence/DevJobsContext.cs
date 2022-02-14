using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    public class DevJobsContext : DbContext
    {
#pragma warning disable CS8618

        public DevJobsContext(DbContextOptions<DevJobsContext> context) : base(context) { }

#pragma warning restore CS8618

        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<JobVacancy>(e =>
            {
                e.HasKey(jv => jv.Id);

                e.HasMany(jv => jv.Applications)
                    .WithOne()
                    .HasForeignKey(ja => ja.IdJobVacancy)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<JobApplication>(e =>
            {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}