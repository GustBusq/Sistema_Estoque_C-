using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Data;

public interface IEstoqueRepository
{
    Task<List<Estoque>> ListarAsync();
    Task AdicionarAsync(Estoque estoque);
    Task ExcluirAsync(int id);
}