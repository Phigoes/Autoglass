using Autoglass.Domain.Entities;
using Autoglass.Infrastructure.Persistence.Repositories;
using Autoglass.Infrastructure;
using Autoglass.UnitTests.Helper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace Autoglass.UnitTests.Infrastructure.Repositories
{
    public class FornecedorRepositorioTests
    {
        [Fact]
        public async void ObterPorId_Retorna_Fornecedor()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();

            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>())).ReturnsAsync(new Fornecedor());
            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            var fornecedor = await fornecedorRepositorio.ObterPorId(1);

            //Assert  
            Assert.NotNull(fornecedor);
            Assert.IsAssignableFrom<Fornecedor>(fornecedor);

            dbSetMock.Verify(c => c.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterPorId_Retorna_Nulo()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();

            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            var fornecedor = await fornecedorRepositorio.ObterPorId(1);

            //Assert  
            Assert.Null(fornecedor);

            dbSetMock.Verify(c => c.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeProdutos()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();
            var fornecedorLista = new List<Fornecedor>()
            {
                new Fornecedor(),
                new Fornecedor(),
                new Fornecedor()
            }.AsQueryable();

            dbSetMock.As<IAsyncEnumerable<Fornecedor>>()
                .Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new AsyncEnumerator<Fornecedor>(fornecedorLista.GetEnumerator()));

            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Produto>(fornecedorLista.Provider));

            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.Expression)
                .Returns(fornecedorLista.Expression);
            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.ElementType)
                .Returns(fornecedorLista.ElementType);
            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => fornecedorLista.GetEnumerator());

            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            var fornecedores = await fornecedorRepositorio.ObterTodos();

            //Assert  
            Assert.NotNull(fornecedores);
            Assert.IsAssignableFrom<IEnumerable<Fornecedor>>(fornecedores);
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeFornecedoresVazia()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();
            var fornecedorLista = new List<Fornecedor>()
            {
            }.AsQueryable();

            dbSetMock.As<IAsyncEnumerable<Fornecedor>>()
                .Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new AsyncEnumerator<Fornecedor>(fornecedorLista.GetEnumerator()));

            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Fornecedor>(fornecedorLista.Provider));

            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.Expression)
                .Returns(fornecedorLista.Expression);
            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.ElementType)
                .Returns(fornecedorLista.ElementType);
            dbSetMock.As<IQueryable<Fornecedor>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => fornecedorLista.GetEnumerator());

            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            var fornecedores = await fornecedorRepositorio.ObterTodos();

            //Assert  
            Assert.False(fornecedores.Any());
            Assert.IsAssignableFrom<IEnumerable<Fornecedor>>(fornecedores);
        }

        [Fact]
        public async void Adicionar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();
            var fornecedor = new Fornecedor();

            dbSetMock.Setup(s => s.AddAsync(It.IsAny<Fornecedor>(), It.IsAny<CancellationToken>()))
                .Callback((Fornecedor model, CancellationToken token) => { })
                .ReturnsAsync(It.IsAny<EntityEntry<Fornecedor>>());

            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            await fornecedorRepositorio.Adicionar(fornecedor);

            //Assert  
            dbSetMock.Verify(c => c.AddAsync(It.IsAny<Fornecedor>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Atualizar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();
            var fornecedor = new Fornecedor();

            dbSetMock.Setup(s => s.Update(It.IsAny<Fornecedor>()))
                .Returns(It.IsAny<EntityEntry<Fornecedor>>());

            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            await fornecedorRepositorio.Atualizar(fornecedor);

            //Assert  
            dbSetMock.Verify(c => c.Update(It.IsAny<Fornecedor>()), Times.Once());
        }

        [Fact]
        public async void Deletar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Fornecedor>>();
            var fornecedor = new Fornecedor();

            dbSetMock.Setup(s => s.Remove(It.IsAny<Fornecedor>()))
                .Returns(It.IsAny<EntityEntry<Fornecedor>>());

            dbContextMock.Setup(s => s.Set<Fornecedor>()).Returns(dbSetMock.Object);

            var fornecedorRepositorio = new FornecedorRepositorio(dbContextMock.Object);

            //Act
            await fornecedorRepositorio.Deletar(fornecedor);

            //Assert  
            dbSetMock.Verify(c => c.Remove(It.IsAny<Fornecedor>()), Times.Once());
        }
    }
}
