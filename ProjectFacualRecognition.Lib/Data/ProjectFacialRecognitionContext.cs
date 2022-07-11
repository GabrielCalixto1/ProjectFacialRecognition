using Microsoft.EntityFrameworkCore;
using ProjectFacualRecognition.Lib.Models;

namespace ProjectFacualRecognition.Lib.Data
{
    public class ProjectFacialRecognitionContext : DbContext
    {
         public ProjectFacialRecognitionContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("fr_users");
            modelBuilder.Entity<User>().HasKey(key => key.Id);
        }
        public DbSet<User> UserDb { get; set; }
    }
}