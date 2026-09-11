using Microsoft.AspNetCore.Mvc;
using SistemaEstoque.Api.Dto;
using SistemaEstoque.Application;
using SistemaEstoque.Domain.Models;

namespace SistemaEstoque.Api.Controllers;

[ApiController]
[Route("api/estoque")]
public class EstoqueController : ControllerBase
{
    private readonly IServiceEstoque _service;

    public EstoqueController(IServiceEstoque service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResponseEstoque>>> Listar()
    {
        var estoque = await _service.ListarEstoqueAsync();

        var response = estoque.Select(e =>
            new ResponseEstoque(e.Id, e.ProdutoId, e.Produto?.Descricao ?? "", e.DataAtualizada, e.Quantidade));

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarEstoqueDto dto)
    {
        try
        {
            var estoque = new Estoque
            {
                ProdutoId = dto.ProdutoId,
                Quantidade = dto.Quantidade,
                DataAtualizada = DateTime.UtcNow
            };

            await _service.CriarEstoqueAsync(estoque);
            return Ok("Estoque registrado com sucesso.");
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
            await _service.ExcluirEstoqueAsync(id);
            return Ok("Registro de estoque excluído com sucesso.");
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}