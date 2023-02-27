using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Domain.Entities.Common.Helper;
using Autoglass.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.API.Controllers
{
    [Route("api/produto")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private IProdutoServico _produtoServico;

        public ProdutoController(IProdutoServico fornecedorServico)
        {
            _produtoServico = fornecedorServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get()
        {
            var produtos = await _produtoServico.ObterTodos();
            if (produtos == null || !produtos.Any())
            {
                return NotFound("Produto(s) não encontrado");
            }

            return Ok(produtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var produto = await _produtoServico.ObterPorId(id);

            if (produto == null)
            {
                return NotFound("Produto não encontrado");
            }

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                var id = await _produtoServico.Adicionar(produtoDTO);

                if (id == 0)
                    return BadRequest("Erro ao adicionar");

                produtoDTO.Id = id;

                return CreatedAtAction(nameof(Get), new { id }, produtoDTO);
            }
            catch (FornecedorNaoExisteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DataDeFabricacaoEhMaiorQueDataDeValidadeException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProdutoDTO produtoDTO)
        {
            try
            {
                if (id != produtoDTO.Id)
                    return BadRequest("Campo de Ids diferente");

                await _produtoServico.Atualizar(produtoDTO);

                return NoContent();
            }
            catch (FornecedorNaoExisteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ProdutoNaoExisteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DataDeFabricacaoEhMaiorQueDataDeValidadeException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = await _produtoServico.ObterPorId(id);
            if (produto == null)
                return NotFound("Produto não encontrado");

            await _produtoServico.Deletar(produto);

            return NoContent();
        }

        [HttpGet("buscar")]
        [ActionName(nameof(BuscarProdutos))]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> BuscarProdutos(
            [FromQuery] FiltroProduto filtro)
        {
            var produtos = await _produtoServico.ObterProdutos(filtro);
            if (produtos == null || !produtos.Any())
                return NotFound("Produto(s) não encontrado");

            return Ok(produtos);
        }
    }
}
