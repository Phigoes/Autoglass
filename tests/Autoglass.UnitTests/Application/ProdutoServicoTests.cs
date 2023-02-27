using Autoglass.Application.DTOs;
using Autoglass.Application.Interfaces.Services;
using Autoglass.Application.Services;
using Autoglass.Domain.Entities;
using Autoglass.Domain.Exceptions;
using Autoglass.Domain.Interfaces.Repositories;
using Autoglass.UnitTests.Helper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Xunit;

namespace Autoglass.UnitTests.Application
{
    public class ProdutoServicoTests
    {
        [Fact]
        public async void ObterPorId_Retorna_Produto()
        {
            //Arrange
            var produto = new Produto("Descricao Teste", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 1);
            var fornecedorDTO = new FornecedorDTO()
            {
                Descricao = "Descricao Teste",
                Situacao = true,
                CNPJ = "1111111111111"
            };

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(produto);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedorDTO);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = await produtoServico.ObterPorId(1);

            //Assert  
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<ProdutoDTO>(resultado);

            produtoRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterPorId_Retorna_Nulo()
        {
            //Arrange
            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(() => null);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(() => null);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = await produtoServico.ObterPorId(1);

            //Assert  
            Assert.Null(resultado);

            produtoRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeFornecedores()
        {
            //Arrange
            var produtos = new List<Produto>
            {
                new Produto("Descricao Teste 1", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 1),
                new Produto("Descricao Teste 2", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 2),
                new Produto("Descricao Teste 3", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 3),
            };

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.ObterTodos()).ReturnsAsync(produtos);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = await produtoServico.ObterTodos();

            //Assert  
            Assert.NotNull(resultado);
            Assert.IsAssignableFrom<List<ProdutoDTO>>(resultado);

            produtoRepositorioMock.Verify(c => c.ObterTodos(), Times.Once());
        }

        [Fact]
        public async void ObterTodos_Retorna_ListaDeProdutosVazia()
        {
            //Arrange
            var produtos = new List<Produto>();

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.ObterTodos()).ReturnsAsync(produtos);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = await produtoServico.ObterTodos();

            //Assert  
            Assert.False(resultado.Any());
            Assert.IsAssignableFrom<List<ProdutoDTO>>(resultado);

            produtoRepositorioMock.Verify(c => c.ObterTodos(), Times.Once());
        }

        [Fact]
        public async void Adicionar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            int fornecedorId = 1;
            var fornecedorDTO = new FornecedorDTO();

            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.AddDays(10).Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Adicionar(It.IsAny<Produto>())).ReturnsAsync(fornecedorId);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedorDTO);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            int id = await produtoServico.Adicionar(produtoDTO);

            //Assert  
            Assert.True(id > 0);

            produtoRepositorioMock.Verify(c => c.Adicionar(It.IsAny<Produto>()), Times.Once());
        }

        [Fact]
        public async void Adicionar_Retorna_FornecedorNaoExisteException()
        {
            //Arrange
            int fornecedorId = 1;

            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Adicionar(It.IsAny<Produto>())).ReturnsAsync(fornecedorId);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(() => null);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = produtoServico.Adicionar(produtoDTO);

            //Assert
            await Assert.ThrowsAsync<FornecedorNaoExisteException>(async () => await resultado);
        }

        [Fact]
        public async void Adicionar_Retorna_DataDeFabricacaoEhMaiorQueDataDeValidadeException()
        {
            //Arrange
            int fornecedorId = 1;
            var fornecedorDTO = new FornecedorDTO();

            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Adicionar(It.IsAny<Produto>())).ReturnsAsync(fornecedorId);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedorDTO);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = produtoServico.Adicionar(produtoDTO);

            //Assert
            await Assert.ThrowsAsync<DataDeFabricacaoEhMaiorQueDataDeValidadeException>(async () => await resultado);
        }

        [Fact]
        public async void Atualizar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var fornecedorDTO = new FornecedorDTO();

            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.AddDays(10).Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedorDTO);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            await produtoServico.Atualizar(produtoDTO);

            //Assert  
            produtoRepositorioMock.Verify(c => c.Atualizar(It.IsAny<Produto>()), Times.Once());
        }

        [Fact]
        public async void Atualizar_Retorna_FornecedorNaoExisteException()
        {
            //Arrange
            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(() => null);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = produtoServico.Atualizar(produtoDTO);

            //Assert
            await Assert.ThrowsAsync<FornecedorNaoExisteException>(async () => await resultado);
        }

        [Fact]
        public async void Atualizar_Retorna_DataDeFabricacaoEhMaiorQueDataDeValidadeException()
        {
            //Arrange
            var fornecedorDTO = new FornecedorDTO();

            var produtoDTO = new ProdutoDTO()
            {
                Id = 1,
                Descricao = "Descricao Teste",
                Situacao = true,
                DataDeFabricacao = DateTime.Now.Date,
                DataDeValidade = DateTime.Now.Date,
            };


            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            fornecedorServicoMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(fornecedorDTO);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            var resultado = produtoServico.Atualizar(produtoDTO);

            //Assert
            await Assert.ThrowsAsync<DataDeFabricacaoEhMaiorQueDataDeValidadeException>(async () => await resultado);
        }

        [Fact]
        public async void Deletar_VerificaSeOMetodoFoiExecutado()
        {
            //Arrange
            var produto = new Produto("Descricao Teste", true, DateTime.Now.Date, DateTime.Now.AddDays(10), 1);
            var produtoDTO = new ProdutoDTO { Id = 1 };

            var produtoRepositorioMock = new Mock<IProdutoRepositorio>();
            var fornecedorServicoMock = new Mock<IFornecedorServico>();
            var mapper = TestMapper.GetMapper();

            produtoRepositorioMock.Setup(f => f.Atualizar(It.IsAny<Produto>())).Returns(Task.CompletedTask);
            produtoRepositorioMock.Setup(f => f.ObterPorId(It.IsAny<int>())).ReturnsAsync(produto);

            var produtoServico = new ProdutoServico(produtoRepositorioMock.Object, fornecedorServicoMock.Object, mapper);

            //Act
            await produtoServico.Deletar(produtoDTO);

            //Assert
            Assert.False(produto.Situacao);
            produtoRepositorioMock.Verify(c => c.Atualizar(It.IsAny<Produto>()), Times.Once());
            produtoRepositorioMock.Verify(c => c.ObterPorId(It.IsAny<int>()), Times.Once());
        }
    }
}
