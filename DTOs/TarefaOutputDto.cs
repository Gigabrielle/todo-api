namespace TodoApi.Models
{
    public class TarefaOutputDto
    {
        public int Id { get; set; }
        public string? Titulo { get; set; }
        public string? Descricao { get; set; }
        public int CategoriaId { get; set; }
        public string? NomeCategoria { get; set; }
    }
}
