using Microsoft.EntityFrameworkCore;

namespace StolenMobilesApi.Models
{
    public class StolenContext : DbContext
    {
        public DbSet<Users> Users { get; set; }
        public DbSet<Mobiles> Mobiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder )
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-7A012LKL\SQLEXPRESS01;Initial Catalog=StolenMobiles;Integrated Security=true");
        }
    }
}
