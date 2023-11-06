using Microsoft.EntityFrameworkCore;

namespace JAKIRO_QATAR.Models
{
    public class JakiroDbContext:DbContext
    {
        public JakiroDbContext(DbContextOptions<JakiroDbContext> options): base(options)
        {
            
        }
        public CartItem CartItem { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
    }
}
