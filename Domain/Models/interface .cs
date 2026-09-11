using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Application;

public interface IServiceEstoque
{
    Task<List<Produto>> ListarProdutosAsync();
    Task<Produto> CriarProdutoAsync(Produto produto);
    Task AtualizarPrecoProdutoAsync(int id, decimal precoNovo);
    Task AtualizarQuantidadeProdutoAsync(int id, int quantidadeNova);
    Task ExcluirProdutoAsync(int id);

    Task<List<Estoque>> ListarEstoqueAsync();
    Task<Estoque> CriarEstoqueAsync(Estoque estoque);
    Task ExcluirEstoqueAsync(int id);
}