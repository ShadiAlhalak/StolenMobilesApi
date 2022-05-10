using Microsoft.EntityFrameworkCore;

namespace StolenMobilesApi.Models
{
    public class StolenContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Mobiles> Mobiles { get; set; }
        public DbSet<Images> Images { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<Mobiles>()
        //        .HasIndex(mob => mob.IMEI)
        //        .IsUnique();
        //    builder.Entity<Users>()
        //        .HasIndex(user => user.Email)
        //        .IsUnique();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-7A012LKL\SQLEXPRESS01;Initial Catalog=StolenMobiles;Integrated Security=true");
        }

    }
}
