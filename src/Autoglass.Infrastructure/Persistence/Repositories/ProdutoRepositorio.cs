using Autoglass.Domain.Entities;
using Autoglass.Domain.Entities.Common.Helper;
using Autoglass.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.Infrastructure.Persistence.Repositories
{
    public class ProdutoRepositorio : RepositorioBase<Produto>, IProdutoRepositorio
    {
        public ProdutoRepositorio(AutoglassDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Produto>> ObterProdutos(FiltroProduto filtroProduto)
        {
            var query = _context.Produtos.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtroProduto.Descricao))
                query = query.Where(p => p.Descricao.Contains(filtroProduto.Descricao));

            query = query.Where(p => p.Situacao == filtroProduto.Situacao);

            if (filtroProduto.DataDeFabricacao.HasValue)
                query = query.Where(p => p.DataDeFabricacao.Date == filtroProduto.DataDeFabricacao.Value);

            if (filtroProduto.DataDeValidade.HasValue)
                query = query.Where(p => p.DataDeValidade.Date == filtroProduto.DataDeValidade.Value);

            var pagina = filtroProduto.Pagina ?? 1;
            var tamanhoPagina = filtroProduto.TamanhoPagina ?? 10;

            var produtos = await query.Skip((pagina - 1) * tamanhoPagina)
                                      .Take(tamanhoPagina)
                                      .ToListAsync();

            return produtos;
        }
    }
}
