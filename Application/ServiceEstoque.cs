using SistemaEstoque.Data;
using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Application;

public class ServiceEstoque : IServiceEstoque
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IEstoqueRepository _estoqueRepository;

    public ServiceEstoque(IProdutoRepository produtoRepository, IEstoqueRepository estoqueRepository)
    {
        _produtoRepository = produtoRepository;
        _estoqueRepository = estoqueRepository;
    }

    public Task<List<Produto>> ListarProdutosAsync() => _produtoRepository.ListarAsync();

    public async Task<Produto> CriarProdutoAsync(Produto produto)
    {
        ArgumentNullException.ThrowIfNull(produto);
        await _produtoRepository.AdicionarAsync(produto);
        return produto;
    }

    public Task AtualizarPrecoProdutoAsync(int id, decimal precoNovo)
    {
        if (precoNovo < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(precoNovo));

        return _produtoRepository.AtualizarPrecoAsync(id, precoNovo);
    }

    public Task AtualizarQuantidadeProdutoAsync(int id, int quantidadeNova)
    {
        if (quantidadeNova < 0)
            throw new ArgumentException("A quantidade não pode ser negativa.", nameof(quantidadeNova));

        return _produtoRepository.AtualizarQuantidadeAsync(id, quantidadeNova);
    }

    public Task ExcluirProdutoAsync(int id) => _produtoRepository.ExcluirAsync(id);

    public Task<List<Estoque>> ListarEstoqueAsync() => _estoqueRepository.ListarAsync();

    public async Task<Estoque> CriarEstoqueAsync(Estoque estoque)
    {
        ArgumentNullException.ThrowIfNull(estoque);

        // Regra de negócio: não faz sentido criar um registro de estoque
        // apontando para um produto que não existe.
        var produtoExiste = await _produtoRepository.ObterPorIdAsync(estoque.ProdutoId);
        if (produtoExiste is null)
            throw new KeyNotFoundException($"Produto {estoque.ProdutoId} não existe.");

        await _estoqueRepository.AdicionarAsync(estoque);
        return estoque;
    }

    public Task ExcluirEstoqueAsync(int id) => _estoqueRepository.ExcluirAsync(id);
}