using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _categoriaService;

        public CategoriaController()
        {
            _categoriaService = new CategoriaService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoriaOutputDto>> Get([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var categorias = _categoriaService.GetAll(skip, take);
            return Ok(categorias);
        }


        [HttpGet("{id}")]
        public ActionResult<CategoriaOutputDto> GetById(int id)
        {
            var categoria = _categoriaService.GetById(id);
            if (categoria == null)
                return NotFound("Categoria não encontrada.");

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<CategoriaOutputDto> Create(CategoriaInputDto novaCategoria)
        {
            if (string.IsNullOrWhiteSpace(novaCategoria.Nome))
                return BadRequest("O nome da categoria é obrigatório.");

            var categoriaCriada = _categoriaService.Create(novaCategoria);
            return CreatedAtAction(nameof(GetById), new { id = categoriaCriada.Id }, categoriaCriada);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoriaInputDto categoriaAtualizada)
        {
            if (string.IsNullOrWhiteSpace(categoriaAtualizada.Nome))
                return BadRequest("O nome da categoria é obrigatório.");

            var atualizou = _categoriaService.Update(id, categoriaAtualizada);
            if (!atualizou)
                return NotFound("Categoria não encontrada.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletou = _categoriaService.Delete(id);
            if (!deletou)
                return NotFound("Categoria não encontrada.");

            return NoContent();
        }
    }
}
