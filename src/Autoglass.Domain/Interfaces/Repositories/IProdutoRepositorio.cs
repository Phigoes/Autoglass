using Autoglass.Domain.Entities;
using Autoglass.Domain.Entities.Common.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces.Repositories
{
    public interface IProdutoRepositorio : IRepositorioBase<Produto>
    {
        Task<IEnumerable<Produto>> ObterProdutos(FiltroProduto filtroProduto);
    }
}
