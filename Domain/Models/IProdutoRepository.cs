namespace SistemaEstoque.Domain.Models;

public interface IProdutoRepository
{
    Task<List<Produto>> ListarAsync();
    Task<Produto?> ObterPorIdAsync(int id);
    Task AdicionarAsync(Produto produto);
    Task AtualizarPrecoAsync(int id, decimal precoNovo);
    Task AtualizarQuantidadeAsync(int id, int quantidadeNova);
    Task ExcluirAsync(int id);
}