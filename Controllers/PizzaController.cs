using ASP.NET.Interfaces;
using ASP.NET.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PizzaController : ControllerBase
    {
        private readonly IPizzaService _pizzaService;
        public PizzaController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pizza>>> GetAll()
        {

            try
            {
                var pizzas = await _pizzaService.GetAll();

                if (pizzas is null)
                {
                    return NotFound("Nunhuma pizza encontrada.");
                }

                return Ok(pizzas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> Get(int id)
        {
            try
            {
                var pizza = await _pizzaService.Get(id);

                if (pizza is null)
                {
                    return NotFound();
                }

                return Ok(pizza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Pizza pizza)
        {
            if (pizza is null)
            {

                return BadRequest("Os dados da pizza não podem ser nulos.");
            }

            try
            {
                await _pizzaService.Add(pizza);
                return CreatedAtAction(nameof(Get), new { id = pizza.Id }, pizza);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Pizza pizza)
        {
            if (pizza == null)
            {
                return BadRequest("Os dados da pizza não podem ser nulos.");
            }


            if (id != pizza.Id)
            {
                return BadRequest("ID da URL não corresponde ao ID da pizza.");
            }

            try
            {
                var updated = await _pizzaService.Update(id, pizza);

                if (!updated)
                {
                    return NotFound("Pizza não encontrada ou erro ao atualizar.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var pizza = await _pizzaService.Get(id);

                if (pizza is null)
                    return NotFound();

                await _pizzaService.Delete(id);


                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno no servidor: {ex.Message}");
            }
        }
    }
}
