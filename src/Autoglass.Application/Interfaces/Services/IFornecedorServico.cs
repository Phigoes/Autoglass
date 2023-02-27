using Autoglass.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Interfaces.Services
{
    public interface IFornecedorServico
    {
        Task<int> Adicionar(FornecedorDTO fornecedorDTO);
        Task Deletar(FornecedorDTO fornecedorDTO);
        Task Atualizar(FornecedorDTO fornecedorDTO);
        Task<FornecedorDTO> ObterPorId(int id);
        Task<IEnumerable<FornecedorDTO>> ObterTodos();
    }
}
