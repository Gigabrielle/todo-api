using TodoApi.Models;

namespace TodoApi.Services
{
    public class TarefaService
    {
        private static List<Tarefa> tarefas = new List<Tarefa>();
        private static List<Categoria> categorias = CategoriaService.Categorias;

        public IEnumerable<TarefaOutputDto> GetAll(int skip = 0, int take = 10)
        {
            return TarefaService.tarefas
                .Skip(skip)
                .Take(take)
                .Select(t => new TarefaOutputDto
                {
                    Id = t.Id,
                    Titulo = t.Titulo,
                    Descricao = t.Descricao,
                    CategoriaId = t.CategoriaId
                });
        }

        public TarefaOutputDto GetById(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return null;

            return new TarefaOutputDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                NomeCategoria = categorias.FirstOrDefault(c => c.Id == tarefa.CategoriaId)?.Nome
            };
        }

        public TarefaOutputDto Create(TarefaInputDto novaTarefa)
        {
            var categoria = categorias.FirstOrDefault(c => c.Id == novaTarefa.CategoriaId);
            if (categoria == null)
                return null;

            var tarefa = new Tarefa
            {
                Id = tarefas.Count > 0 ? tarefas.Max(t => t.Id) + 1 : 1,
                Titulo = novaTarefa.Titulo,
                Descricao = novaTarefa.Descricao,
                CategoriaId = novaTarefa.CategoriaId
            };

            tarefas.Add(tarefa);

            return new TarefaOutputDto
            {
                Id = tarefa.Id,
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                NomeCategoria = categoria.Nome
            };
        }

        public bool Update(int id, TarefaInputDto tarefaAtualizada)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return false;

            tarefa.Titulo = tarefaAtualizada.Titulo;
            tarefa.Descricao = tarefaAtualizada.Descricao;
            tarefa.CategoriaId = tarefaAtualizada.CategoriaId;

            return true;
        }

        public bool Delete(int id)
        {
            var tarefa = tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
                return false;

            tarefas.Remove(tarefa);
            return true;
        }
    }
}
