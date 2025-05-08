using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly TarefaService _tarefaService;

        public TarefaController()
        {
            _tarefaService = new TarefaService();
        }

        [HttpGet]
        public ActionResult<IEnumerable<TarefaOutputDto>> Get([FromQuery] int skip = 0, [FromQuery] int take = 10)
        {
            var tarefas = _tarefaService.GetAll(skip, take);
            return Ok(tarefas);
        }


        [HttpGet("{id}")]
        public ActionResult<TarefaOutputDto> GetById(int id)
        {
            var tarefa = _tarefaService.GetById(id);
            if (tarefa == null)
                return NotFound("Tarefa não encontrada.");

            return Ok(tarefa);
        }

        [HttpPost]
        public ActionResult<TarefaOutputDto> Create(TarefaInputDto novaTarefa)
        {
            if (string.IsNullOrWhiteSpace(novaTarefa.Titulo))
                return BadRequest("O título é obrigatório.");

            var tarefaCriada = _tarefaService.Create(novaTarefa);
            return CreatedAtAction(nameof(GetById), new { id = tarefaCriada.Id }, tarefaCriada);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TarefaInputDto tarefaAtualizada)
        {
            if (string.IsNullOrWhiteSpace(tarefaAtualizada.Titulo))
                return BadRequest("O título é obrigatório.");

            var atualizou = _tarefaService.Update(id, tarefaAtualizada);
            if (!atualizou)
                return NotFound("Tarefa não encontrada.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deletou = _tarefaService.Delete(id);
            if (!deletou)
                return NotFound("Tarefa não encontrada.");

            return NoContent();
        }
    }
}
