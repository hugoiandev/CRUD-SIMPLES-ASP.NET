using ASP.NET.Models;

namespace ASP.NET.Interfaces
{
    public interface IPizzaService
    {
        Task<List<Pizza>> GetAll();

        Task<Pizza?> Get(int id);

        Task<Pizza> Add(Pizza newPizza);

        Task<bool> Delete(int id);

        Task<bool> Update(int pizzaId, Pizza newPizza);
    }
}
