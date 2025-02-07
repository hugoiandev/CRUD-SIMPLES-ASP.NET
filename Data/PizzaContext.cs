using ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Data
{
    public class PizzaContext : DbContext
    {
        public PizzaContext(DbContextOptions<PizzaContext> options) : base(options)
        {
        }

        public DbSet<Pizza> Pizzas => Set<Pizza>();

    }
}
