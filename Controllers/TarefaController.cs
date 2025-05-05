using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private static List<Tarefa> tarefas = new List<Tarefa>();

        /// <summary>
        /// Obtém todas as tarefas com o nome da categoria.
        /// </summary>
        /// <returns>Lista de tarefas com nome da categoria</returns>
        [HttpGet]
        public IActionResult Get()
        {
            var resposta = tarefas.Select(t => new TarefaOutputDto
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descricao = t.Descricao,
                CategoriaId = t.CategoriaId,
                NomeCategoria = CategoriaController.categorias!
                    .FirstOrDefault(c => c.Id == t.CategoriaId)?.Nome

            }).ToList();

            return Ok(resposta);
            
        }
        /// <summary>
        /// Obtém uma tarefa por ID.
        /// </summary>
        /// <param name="id">O ID da tarefa</param>
        /// <returns>A tarefa correspondente ou NotFound se não encontrada</returns>
        [HttpGet("{id}")]
        public ActionResult<Tarefa> GetById(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound($"Nenhuma tarefa encontrada com o ID {id}.");
            }
            return tarefa;
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="novaTarefa">A tarefa a ser criada</param>
        /// <returns>A tarefa criada</returns>
        [HttpPost]
        public ActionResult<Tarefa> Create([FromBody] TarefaInputDto novaTarefaDto)
        {
            if (string.IsNullOrEmpty(novaTarefaDto.Titulo))
            {
                return BadRequest("O título da tarefa é obrigatório.");
            }

            var categoria = CategoriaController.categorias
                 .FirstOrDefault(c => c.Id == novaTarefaDto.CategoriaId);

            if (categoria == null)
            {
                return BadRequest("A categoria informada não existe. Cadastre uma categoria válida.");
            }

            var novaTarefa = new Tarefa
            {
                Id = tarefas.Count > 0 ? tarefas.Max(t => t.Id) + 1 : 1,
                Titulo = novaTarefaDto.Titulo,
                Descricao = novaTarefaDto.Descricao,
                CategoriaId = novaTarefaDto.CategoriaId
            };

            tarefas.Add(novaTarefa);

            return CreatedAtAction(nameof(GetById), new { id = novaTarefa.Id }, novaTarefa);
        }


        /// <summary>
        /// Atualiza uma tarefa existente
        /// </summary>
        /// <param name="id">O ID da tarefa a ser atualizada</param>
        /// <param name="tarefaAtualizada">A tarefa a ser atualizada</param>
        /// /// <returns>A tarefa atualizada</returns>
        [HttpPut("{id}")]
        public ActionResult<Tarefa> Update(int id, Tarefa tarefaAtualizada)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound($"Nenhuma tarefa encontrada com o ID {id}.");
            }

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.Concluida = tarefaAtualizada.Concluida;

            return Ok(tarefa);
        }
        /// <summary>
        /// Remove uma tarefa existente.
        /// </summary>
        /// <param name="id">O ID da tarefa a ser removida</param>
        /// <returns>204 No Content se a tarefa for removida com sucesso, ou 404 Not Found se a tarefa não for encontrada</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound($"Nenhuma tarefa encontrada com o ID {id}.");
            }

            tarefas.Remove(tarefa);
            return NoContent();
        }
    }
}