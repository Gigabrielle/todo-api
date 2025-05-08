using TodoApi.Models;

namespace TodoApi.Services
{
    public class CategoriaService
    {
        public static List<Categoria> Categorias { get; set; } = new List<Categoria>();

        public IEnumerable<CategoriaOutputDto> GetAll(int skip = 0, int take = 10)
        {
            return CategoriaService.Categorias
                .Skip(skip)
                .Take(take)
                .Select(c => new CategoriaOutputDto
                {
                    Id = c.Id,
                    Nome = c.Nome
                });
        }


        public CategoriaOutputDto GetById(int id)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return null;

            return new CategoriaOutputDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public CategoriaOutputDto Create(CategoriaInputDto novaCategoria)
        {
            var categoria = new Categoria
            {
                Id = Categorias.Count > 0 ? Categorias.Max(c => c.Id) + 1 : 1,
                Nome = novaCategoria.Nome
            };

            Categorias.Add(categoria);

            return new CategoriaOutputDto
            {
                Id = categoria.Id,
                Nome = categoria.Nome
            };
        }

        public bool Update(int id, CategoriaInputDto categoriaAtualizada)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return false;

            categoria.Nome = categoriaAtualizada.Nome;
            return true;
        }

        public bool Delete(int id)
        {
            var categoria = Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return false;

            Categorias.Remove(categoria);
            return true;
        }
    }
}
