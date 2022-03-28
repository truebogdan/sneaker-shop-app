using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBRepo
{
    public class DbShopContext : DbContext
    {
        public DbShopContext(DbContextOptions<DbShopContext> options) : base(options)
        {
            
        }
            public DbSet<CartProductModel> CartProducts { get; set; }

    }
}
