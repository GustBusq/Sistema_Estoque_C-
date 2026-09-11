using Microsoft.EntityFrameworkCore;
using SistemaEstoque.Domain.Models;
using System.Reflection.Emit;

namespace SistemaEstoque.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Estoque> Estoques => Set<Estoque>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Produto>().ToTable("Produtos");

        modelBuilder.Entity<Estoque>(entity =>
        {
            entity.ToTable("Estoque");

            // Relacionamento: um Produto pode ter vários registros de Estoque.
            // Se o Produto for excluído, os registros de Estoque ligados a ele também são (Cascade).
            entity
                .HasOne(e => e.Produto)
                .WithMany()
                .HasForeignKey(e => e.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}