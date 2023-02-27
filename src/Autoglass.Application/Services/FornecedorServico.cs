using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Exceptions;
using Autoglass.Domain.Interfaces.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Services
{
    public class FornecedorServico : IFornecedorServico
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IMapper _mapper;

        public FornecedorServico(IFornecedorRepositorio fornecedorRepositorio, IMapper mapper)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
            _mapper = mapper;
        }

        public async Task<int> Adicionar(FornecedorDTO fornecedorDTO)
        {
            return await _fornecedorRepositorio.Adicionar(_mapper.Map<Fornecedor>(fornecedorDTO));
        }

        public async Task Atualizar(FornecedorDTO fornecedorDTO)
        {
            var fornecedor = await _fornecedorRepositorio.ObterPorId(fornecedorDTO.Id);
            if (fornecedor == null)
                throw new FornecedorNaoExisteException();

            await _fornecedorRepositorio.Atualizar(_mapper.Map<Fornecedor>(fornecedorDTO));
        }

        public async Task Deletar(FornecedorDTO fornecedorDTO)
        {
            var fornecedor = await _fornecedorRepositorio.ObterPorId(fornecedorDTO.Id);
            fornecedor.Inativar();

            await _fornecedorRepositorio.Atualizar(fornecedor);
        }

        public async Task<FornecedorDTO> ObterPorId(int id)
        {
            return _mapper.Map<FornecedorDTO>(await _fornecedorRepositorio.ObterPorId(id));
        }

        public async Task<IEnumerable<FornecedorDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FornecedorDTO>>(await _fornecedorRepositorio.ObterTodos());
        }
    }
}
