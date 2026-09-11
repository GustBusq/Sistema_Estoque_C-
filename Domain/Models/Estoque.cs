namespace SistemaEstoque.Domain.Models;

public class Estoque
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public DateTime DataAtualizada { get; set; } = DateTime.UtcNow;
    public int Quantidade { get; set; }

    // Navegação para o Produto (usada pelo EF Core para o JOIN).
    // Assim não guardamos uma cópia da descrição aqui dentro.
    public Produto? Produto { get; set; }
}