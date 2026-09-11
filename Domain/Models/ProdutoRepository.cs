using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Data;

namespace SistemaEstoque.Domain.Models;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context) => _context = context;

    public Task<List<Produto>> ListarAsync() =>
        _context.Produtos.AsNoTracking().ToListAsync();

    public Task<Produto?> ObterPorIdAsync(int id) =>
        _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarPrecoAsync(int id, decimal precoNovo)
    {
        var produto = await _context.Produtos.FindAsync(id)
            ?? throw new KeyNotFoundException($"Produto {id} não encontrado.");

        produto.Preco = precoNovo;
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarQuantidadeAsync(int id, int quantidadeNova)
    {
        var produto = await _context.Produtos.FindAsync(id)
            ?? throw new KeyNotFoundException($"Produto {id} não encontrado.");

        produto.Quantidade = quantidadeNova;
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var produto = await _context.Produtos.FindAsync(id)
            ?? throw new KeyNotFoundException($"Produto {id} não encontrado.");

        _context.Produtos.Remove(produto);
        await _context.SaveChangesAsync();
    }
}