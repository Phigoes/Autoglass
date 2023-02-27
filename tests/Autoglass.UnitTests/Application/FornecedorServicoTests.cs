using Autoglass.Application.DTOs;
using Autoglass.Application.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.UnitTests.Helper;
using Moq;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using System.Linq;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Domain.Exceptions;

namespace Autoglass.UnitTests.Application
{
    public class FornecedorServicoTests
    {
        [Fact]
        public async void ObterPorId_Retorna_Fornecedor()
        {
            //Arrange
            var fornecedor = new Fornecedor("Descricao Teste", true, "1111111111111");

            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterPorId(1)).ReturnsAsync(fornecedor);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            var resultado = await fornecedorServico.ObterPorId(1);

            //Assert  
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<FornecedorDTO>(resultado);

            fornecedorRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterPorId_Retorna_Nulo()
        {
            //Arrange
            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterPorId(1)).ReturnsAsync(() => null);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            var resultado = await fornecedorServico.ObterPorId(1);

            //Assert  
            Assert.Null(resultado);

            fornecedorRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeFornecedores()
        {
            //Arrange
            var fornecedores = new List<Fornecedor>
            {
                new Fornecedor("Descricao Teste 1", true, "1111111111111"),
                new Fornecedor("Descricao Teste 2", true, "2222222222222"),
                new Fornecedor("Descricao Teste 3", true, "3333333333333"),
            };

            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterTodos()).ReturnsAsync(fornecedores);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            var resultado = await fornecedorServico.ObterTodos();

            //Assert  
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<List<FornecedorDTO>>(resultado);

            fornecedorRepositorioMock.Verify(c => c.ObterTodos(), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeFornecedoresVazia()
        {
            //Arrange
            var fornecedores = new List<Fornecedor>();

            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterTodos()).ReturnsAsync(fornecedores);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            var resultado = await fornecedorServico.ObterTodos();

            //Assert  
            Assert.False(resultado.Any());
            Assert.IsAssignableFrom<List<FornecedorDTO>>(resultado);

            fornecedorRepositorioMock.Verify(c => c.ObterTodos(), Times.Once());
        }

        [Fact]
        public async void Adicionar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            int fornecedorId = 1;

            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.Adicionar(It.IsAny<Fornecedor>())).Returns(Task.FromResult(fornecedorId));

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            int id = await fornecedorServico.Adicionar(It.IsAny<FornecedorDTO>());

            //Assert  
            Assert.True(id > 0);

            fornecedorRepositorioMock.Verify(c => c.Adicionar(It.IsAny<Fornecedor>()), Times.Once());
        }

        [Fact]
        public async void Atualizar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var fornecedor = new Fornecedor();
            var fornecedorDTO = new FornecedorDTO() { Id = 1 };
            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedor);
            fornecedorRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            await fornecedorServico.Atualizar(fornecedorDTO);

            //Assert
            fornecedorRepositorioMock.Verify(c => c.Atualizar(It.IsAny<Fornecedor>()), Times.Once());
        }

        [Fact]
        public async void Atualizar_Retorna_FornecedorNaoExisteException()
        {
            //Arrange
            var fornecedorDTO = new FornecedorDTO { Id = 1 };
            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(() => null);
            fornecedorRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            var resultado = fornecedorServico.Atualizar(fornecedorDTO);

            //Assert
            await Assert.ThrowsAsync<FornecedorNaoExisteException>(async () => await resultado);
        }

        [Fact]
        public async void Deletar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var fornecedor = new Fornecedor("Descricao Teste", true, "1111111111111");
            var fornecedorDTO = new FornecedorDTO { Id = 1 };
            var fornecedorRepositorioMock = new Mock<IFornecedorRepositorio>();
            var mapper = TestMapper.GetMapper();

            fornecedorRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedor);
            fornecedorRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Fornecedor>())).Returns(Task.CompletedTask);

            var fornecedorServico = new FornecedorServico(fornecedorRepositorioMock.Object, mapper);

            //Act
            await fornecedorServico.Deletar(fornecedorDTO);

            //Assert
            Assert.False(fornecedor.Situacao);
            fornecedorRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
            fornecedorRepositorioMock.Verify(c => c.Atualizar(It.IsAny<Fornecedor>()), Times.Once());
        }
    }
}
