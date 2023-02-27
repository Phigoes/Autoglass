using Autoglass.Domain.Entities;
using Autoglass.Infrastructure;
using Autoglass.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Xunit;
using Autoglass.UnitTests.Helper;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Autoglass.UnitTests.Infrastructure.Repositories
{
    public class ProdutoRepositorioTests
    {
        [Fact]
        public async void ObterPorId_Retorna_Produto()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();

            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>())).ReturnsAsync(new Produto());
            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            var produto = await produtoRepositorio.ObterPorId(1);

            //Assert  
            Assert.NotNull(produto);
            Assert.IsAssignableFrom<Produto>(produto);

            dbSetMock.Verify(c => c.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterPorId_Retorna_Nulo()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();

            dbSetMock.Setup(s => s.FindAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            var produto = await produtoRepositorio.ObterPorId(1);

            //Assert  
            Assert.Null(produto);

            dbSetMock.Verify(c => c.FindAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeProdutos()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();
            var produtoLista = new List<Produto>()
            {
                new Produto(),
                new Produto(),
                new Produto()
            }.AsQueryable();

            dbSetMock.As<IAsyncEnumerable<Produto>>()
                .Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new AsyncEnumerator<Produto>(produtoLista.GetEnumerator()));

            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Produto>(produtoLista.Provider));

            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.Expression)
                .Returns(produtoLista.Expression);
            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.ElementType)
                .Returns(produtoLista.ElementType);
            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => produtoLista.GetEnumerator());

            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            var produtos = await produtoRepositorio.ObterTodos();

            //Assert  
            Assert.NotNull(produtos);
            Assert.IsAssignableFrom<IEnumerable<Produto>>(produtos);
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeProdutosVazia()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();
            var produtoLista = new List<Produto>()
            {
            }.AsQueryable();

            dbSetMock.As<IAsyncEnumerable<Produto>>()
                .Setup(d => d.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
                .Returns(new AsyncEnumerator<Produto>(produtoLista.GetEnumerator()));

            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<Produto>(produtoLista.Provider));

            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.Expression)
                .Returns(produtoLista.Expression);
            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.ElementType)
                .Returns(produtoLista.ElementType);
            dbSetMock.As<IQueryable<Produto>>()
                .Setup(m => m.GetEnumerator())
                .Returns(() => produtoLista.GetEnumerator());

            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            var produtos = await produtoRepositorio.ObterTodos();

            //Assert  
            Assert.False(produtos.Any());
            Assert.IsAssignableFrom<IEnumerable<Produto>>(produtos);
        }

        [Fact]
        public async void Adicionar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();
            var produto = new Produto();

            dbSetMock.Setup(s => s.AddAsync(It.IsAny<Produto>(), It.IsAny<CancellationToken>()))
                .Callback((Produto model, CancellationToken token) => { })
                .ReturnsAsync(It.IsAny<EntityEntry<Produto>>());

            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            await produtoRepositorio.Adicionar(produto);

            //Assert  
            dbSetMock.Verify(c => c.AddAsync(It.IsAny<Produto>(), It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void Atualizar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();
            var produto = new Produto();

            dbSetMock.Setup(s => s.Update(It.IsAny<Produto>()))
                .Returns(It.IsAny<EntityEntry<Produto>>());

            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            await produtoRepositorio.Atualizar(produto);

            //Assert  
            dbSetMock.Verify(c => c.Update(It.IsAny<Produto>()), Times.Once());
        }

        [Fact]
        public async void Deletar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var dbContextMock = new Mock<AutoglassDbContext>();
            var dbSetMock = new Mock<DbSet<Produto>>();
            var produto = new Produto();

            dbSetMock.Setup(s => s.Remove(It.IsAny<Produto>()))
                .Returns(It.IsAny<EntityEntry<Produto>>());

            dbContextMock.Setup(s => s.Set<Produto>()).Returns(dbSetMock.Object);

            var produtoRepositorio = new ProdutoRepositorio(dbContextMock.Object);

            //Act
            await produtoRepositorio.Deletar(produto);

            //Assert  
            dbSetMock.Verify(c => c.Remove(It.IsAny<Produto>()), Times.Once());
        }
    }
}