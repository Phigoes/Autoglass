using Autoglass.Application.DTOs;
using Autoglass.Domain.Entities.Common.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Interfaces.Services
{
    public interface IProdutoServico
    {
        Task<int> Adicionar(ProdutoDTO produto);
        Task Deletar(ProdutoDTO produto);
        Task Atualizar(ProdutoDTO produto);
        Task<ProdutoDTO> ObterPorId(int id);
        Task<IEnumerable<ProdutoDTO>> ObterTodos();
        Task<IEnumerable<ProdutoDTO>> ObterProdutos(FiltroProduto filtroProduto);
    }
}
