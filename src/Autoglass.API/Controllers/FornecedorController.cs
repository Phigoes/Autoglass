using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.API.Controllers
{
    [Route("api/fornecedores")]
    [ApiController]
    public class FornecedorController : ControllerBase
    {
        private IFornecedorServico _fornecedorServico;

        public FornecedorController(IFornecedorServico fornecedorServico)
        {
            _fornecedorServico = fornecedorServico;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorDTO>>> Get()
        {
            var fornecedores = await _fornecedorServico.ObterTodos();
            if (fornecedores == null || !fornecedores.Any())
                return NotFound("Fornecedor(es) não encontrado");

            return Ok(fornecedores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var fornecedor = await _fornecedorServico.ObterPorId(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado");

            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FornecedorDTO fornecedorDTO)
        {
            var id =  await _fornecedorServico.Adicionar(fornecedorDTO);

            if (id == 0)
                return BadRequest("Erro ao adicionar");

            fornecedorDTO.Id = id;

            return CreatedAtAction(nameof(Get), new { id }, fornecedorDTO);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FornecedorDTO fornecedorDTO)
        {
            try
            {
                if (id != fornecedorDTO.Id)
                    return BadRequest();

                await _fornecedorServico.Atualizar(fornecedorDTO);

                return NoContent();
            }
            catch (FornecedorNaoExisteException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var fornecedor = await _fornecedorServico.ObterPorId(id);
            if (fornecedor == null)
                return NotFound("Fornecedor não encontrado");

            await _fornecedorServico.Deletar(fornecedor);

            return NoContent();
        }
    }
}
