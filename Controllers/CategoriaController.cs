using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        public static List<Categoria> categorias {get; set; } = new List<Categoria>();

        [HttpGet]
        public ActionResult<List<Categoria>> GetAll()
        {
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public ActionResult<Categoria> GetById(int id)
        {
            var categoria = categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null) return NotFound("Categoria não encontrada.");
            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<Categoria> Create(Categoria novaCategoria)
        {
            novaCategoria.Id = categorias.Count > 0 ? categorias.Max(c => c.Id) + 1 : 1;
            categorias.Add(novaCategoria);
            return CreatedAtAction(nameof(GetById), new { id = novaCategoria.Id }, novaCategoria);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Categoria categoriaAtualizada)
        {
            var categoria = categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            categoria.Nome = categoriaAtualizada.Nome;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoria = categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null) return NotFound("Categoria não encontrada.");

            categorias.Remove(categoria);
            return NoContent();
        }
    }
}
