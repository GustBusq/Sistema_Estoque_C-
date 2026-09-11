using System.ComponentModel.DataAnnotations;

namespace SistemaEstoque.Api.Dto;

// Sem "Id" aqui: quem cria um produto não escolhe o Id, o banco gera automaticamente.
public record CriarProdutoDto(
    [property: Required(ErrorMessage = "Descricao é um campo obrigatório.")]
    string Descricao,

    [property: Range(0, double.MaxValue, ErrorMessage = "Preco não pode ser negativo.")]
    decimal Preco,

    [property: Range(0, int.MaxValue, ErrorMessage = "Quantidade não pode ser negativa.")]
    int Quantidade
);

public record CriarEstoqueDto(
    [property: Required(ErrorMessage = "ProdutoId é um campo obrigatório.")]
    int ProdutoId,

    [property: Range(0, int.MaxValue, ErrorMessage = "Quantidade não pode ser negativa.")]
    int Quantidade
);

public record ResponseProduto(
    int Id,
    DateTime Data,
    string Descricao,
    decimal Preco,
    int Quantidade
);

public record ResponseEstoque(
    int Id,
    int ProdutoId,
    string ProdutoDescricao,
    DateTime DataAtualizada,
    int Quantidade
);