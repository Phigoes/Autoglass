using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.Infrastructure.Persistence.Repositories
{
    public abstract class RepositorioBase<T> : IRepositorioBase<T> where T : EntidadeBase
    {
        protected readonly AutoglassDbContext _context;

        public RepositorioBase(AutoglassDbContext context)
        {
            _context = context;
        }

        public async Task<T> ObterPorId(int id)
        {
            var entidade = await _context.Set<T>().FindAsync(id);
            return entidade;
        }

        public virtual async Task<IEnumerable<T>> ObterTodos()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<int> Adicionar(T entidade)
        {
            await _context.Set<T>().AddAsync(entidade);
            await _context.SaveChangesAsync();

            return entidade.Id;
        }

        public async Task Atualizar(T entidade)
        {
            _context.Set<T>().Update(entidade);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Deletar(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            await _context.SaveChangesAsync();
        }
    }
}
