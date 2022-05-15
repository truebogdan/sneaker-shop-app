using Microsoft.EntityFrameworkCore;

namespace DBRepo
{
    public class DbShopContext : DbContext
    {
        public DbShopContext(DbContextOptions<DbShopContext> options) : base(options)
        {

        }
        public DbSet<CartProductModel> CartProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct>? OrderProducts { get; set; }

    }
}
