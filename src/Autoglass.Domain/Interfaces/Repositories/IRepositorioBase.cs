using Autoglass.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces.Repositories
{
    public interface IRepositorioBase<T> where T : EntidadeBase
    {
        Task<T> ObterPorId(int id);
        Task<IEnumerable<T>> ObterTodos();
        Task<int> Adicionar(T entidade);
        Task Atualizar(T entidade);
        Task Deletar(T entidade);
    }
}
