using Autoglass.Domain.Entities;
using System;
using Xunit;

namespace Autoglass.UnitTests.Domain.Entities
{
    public class ProdutoTests
    {
        [Fact]
        public void CriarProdutoComTodosOsCamposPreenchidos()
        {
            var produto = new Produto("Descricao Teste", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 1);

            Assert.NotEmpty(produto.Descricao);
            Assert.True(produto.Situacao);
            Assert.Equal(produto.DataDeFabricacao, DateTime.Now.Date);
            Assert.Equal(produto.DataDeValidade, DateTime.Now.AddDays(10).Date);
            Assert.True(produto.IdFornecedor > 0);
        }

        [Fact]
        public void Inativar_Retorna_SituacaoInativo()
        {
            var produto = new Produto("Descricao Teste", true, DateTime.Now.Date, DateTime.Now.AddDays(10).Date, 1);
            produto.Inativar();

            Assert.False(produto.Situacao);
        }
    }
}
