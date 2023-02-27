using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;

namespace Autoglass.Infrastructure.Persistence.Repositories
{
    public class FornecedorRepositorio : RepositorioBase<Fornecedor>, IFornecedorRepositorio
    {
        public FornecedorRepositorio(AutoglassDbContext context) : base(context)
        {
        }
    }
}
