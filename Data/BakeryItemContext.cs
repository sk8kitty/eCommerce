using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data
{
    public class BakeryItemContext : DbContext
    {
        public BakeryItemContext(DbContextOptions<BakeryItemContext> options) : base(options) 
        {

        }

        public DbSet<Item> Items { get; set; }

        public DbSet<Member> Members { get; set; }
    }
}
