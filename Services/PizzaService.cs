using ASP.NET.Data;
using ASP.NET.Interfaces;
using ASP.NET.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.Services
{
    public class PizzaService : IPizzaService
    {
        private readonly PizzaContext _context;

        public PizzaService(PizzaContext context)
        {
            _context = context;
        }

        public async Task<List<Pizza>> GetAll()
        {
            return await _context.Pizzas.AsNoTracking().ToListAsync();
        }

        public async Task<Pizza?> Get(int id)
        {
            return await _context.Pizzas.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pizza> Add(Pizza newPizza)
        {
            await _context.Pizzas.AddAsync(newPizza);
            await _context.SaveChangesAsync();

            return newPizza;
        }

        public async Task<bool> Delete(int id)
        {
            var pizzaToDelete = await _context.Pizzas.FindAsync(id);

            if (pizzaToDelete is not null)
            {
                _context.Pizzas.Remove(pizzaToDelete);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Update(int pizzaId, Pizza newPizza)
        {
            var pizzaToUpdate = await _context.Pizzas.AsNoTracking().FirstOrDefaultAsync(p => p.Id == pizzaId);

            if (pizzaToUpdate is null)
            {
                throw new InvalidOperationException("Pizza does not exist");
            }

            _context.Update(newPizza);

            return await _context.SaveChangesAsync() > 0;

        
        }
    }
}
