using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Entities.Common.Helper;
using Autoglass.Domain.Exceptions;
using Autoglass.Domain.Interfaces.Repositories;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Services
{
    public class ProdutoServico : IProdutoServico
    {
        private readonly IFornecedorServico _fornecedorServico;
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IMapper _mapper;

        public ProdutoServico(IProdutoRepositorio produtoRepositorio, IFornecedorServico fornecedorServico, IMapper mapper)
        {
            _produtoRepositorio = produtoRepositorio;
            _fornecedorServico = fornecedorServico;
            _mapper = mapper;
        }

        public async Task<int> Adicionar(ProdutoDTO produtoDTO)
        {
            var fornecedor = await _fornecedorServico.ObterPorId(produtoDTO.IdFornecedor);
            if (fornecedor == null) throw new FornecedorNaoExisteException();

            var produto = _mapper.Map<Produto>(produtoDTO);
            if (!produto.CompararDataDeFabricacaoComDataDeValidade())
                throw new DataDeFabricacaoEhMaiorQueDataDeValidadeException();

            return await _produtoRepositorio.Adicionar(produto);
        }

        public async Task Atualizar(ProdutoDTO produtoDTO)
        {
            var fornecedor = await _fornecedorServico.ObterPorId(produtoDTO.IdFornecedor);
            if (fornecedor == null) throw new FornecedorNaoExisteException();

            var produto = await _produtoRepositorio.ObterPorId(produtoDTO.Id);
            if (produto == null) throw new ProdutoNaoExisteException();

            if (!produto.CompararDataDeFabricacaoComDataDeValidade())
                throw new DataDeFabricacaoEhMaiorQueDataDeValidadeException();

            await _produtoRepositorio.Atualizar(_mapper.Map<Produto>(produtoDTO));
        }

        public async Task Deletar(ProdutoDTO produtoDTO)
        {
            var produto = await _produtoRepositorio.ObterPorId(produtoDTO.Id);
            produto.Inativar();

            await _produtoRepositorio.Atualizar(_mapper.Map<Produto>(produto));
        }

        public async Task<ProdutoDTO> ObterPorId(int id)
        {
            return _mapper.Map<ProdutoDTO>(await _produtoRepositorio.ObterPorId(id));
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepositorio.ObterTodos());
        }

        public async Task<IEnumerable<ProdutoDTO>> ObterProdutos(FiltroProduto filtroProduto)
        {
            return _mapper.Map<IEnumerable<ProdutoDTO>>(await _produtoRepositorio.ObterProdutos(filtroProduto));
        }
    }
}
