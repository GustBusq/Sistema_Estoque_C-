using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Data;

public class EstoqueRepository : IEstoqueRepository
{
    private readonly AppDbContext _context;

    public EstoqueRepository(AppDbContext context) => _context = context;

    public Task<List<Estoque>> ListarAsync() =>
        // Include() traz o Produto relacionado junto (equivalente a um JOIN),
        // para que a descrição do produto esteja disponível sem duplicar dado no banco.
        _context.Estoques.AsNoTracking().Include(e => e.Produto).ToListAsync();

    public async Task AdicionarAsync(Estoque estoque)
    {
        _context.Estoques.Add(estoque);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var estoque = await _context.Estoques.FindAsync(id)
            ?? throw new KeyNotFoundException($"Registro de estoque {id} não encontrado.");

        _context.Estoques.Remove(estoque);
        await _context.SaveChangesAsync();
    }
}