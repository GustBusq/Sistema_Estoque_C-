using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Api.Dto;
using SistemaEstoque.Application;
using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Api.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutoController : ControllerBase
{
    private readonly IServiceEstoque _service;

    public ProdutoController(IServiceEstoque service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseProduto>>> Listar()
    {
        var produtos = await _service.ListarProdutosAsync();

        var response = produtos.Select(p =>
            new ResponseProduto(p.Id, p.Data, p.Descricao, p.Preco, p.Quantidade));

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProdutoDto dto)
    {
        var produto = new Produto
        {
            Descricao = dto.Descricao,
            Preco = dto.Preco,
            Quantidade = dto.Quantidade
        };

        await _service.CriarProdutoAsync(produto);

        return CreatedAtAction(nameof(Listar), new { id = produto.Id },
            new ResponseProduto(produto.Id, produto.Data, produto.Descricao, produto.Preco, produto.Quantidade));
    }

    [HttpPut("{id:int}/preco")]
    public async Task<IActionResult> AtualizarPreco(int id, [FromBody] decimal precoNovo)
    {
        try
        {
            await _service.AtualizarPrecoProdutoAsync(id, precoNovo);
            return Ok("Preço atualizado com sucesso.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut("{id:int}/quantidade")]
    public async Task<IActionResult> AtualizarQuantidade(int id, [FromBody] int quantidadeNova)
    {
        try
        {
            await _service.AtualizarQuantidadeProdutoAsync(id, quantidadeNova);
            return Ok("Quantidade atualizada com sucesso.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        try
        {
            await _service.ExcluirProdutoAsync(id);
            return Ok("Produto excluído com sucesso.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}