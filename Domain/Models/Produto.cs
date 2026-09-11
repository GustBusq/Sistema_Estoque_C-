namespace SistemaEstoque.Domain.Models;

public class Produto
{
    public int Id { get; set; }
    public DateTime Data { get; set; } = DateTime.UtcNow;
    public required string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
}