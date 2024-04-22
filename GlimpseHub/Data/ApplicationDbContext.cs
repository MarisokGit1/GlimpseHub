using GlimpseHub.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlimpseHub.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.Migrate();

        }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserLikePicture> UserLikePictures { get; set; }
        public DbSet<Rating> Ratings { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLikePicture>()
                .HasKey(x => new { x.AppUserId, x.PictureId });

            modelBuilder.Entity<Picture>()
                .HasOne(x => x.Gallery)
                .WithMany(x => x.Pictures)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
               .HasOne(x => x.User)
               .WithMany(x => x.Ratings)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Rating>()
               .HasOne(x => x.Gallery)
               .WithMany(x => x.Ratings)
               .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);


        }
    }

}
