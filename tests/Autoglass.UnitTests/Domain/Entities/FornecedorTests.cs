using Autoglass.Domain.Entities;
using Xunit;

namespace Autoglass.UnitTests.Domain.Entities
{
    public class FornecedorTests
    {
        [Fact]
        public void CriarFornecedorComTodosOsCamposPreenchidos()
        {
            var fornecedor = new Fornecedor("Descricao Teste", true, "1111111111111");

            Assert.NotEmpty(fornecedor.Descricao);
            Assert.True(fornecedor.Situacao);
            Assert.NotEmpty(fornecedor.CNPJ);
        }

        [Fact]
        public void Inativar_Retorna_SituacaoInativo()
        {
            var fornecedor = new Fornecedor("Descricao Teste", true, "1111111111111");
            fornecedor.Inativar();

            Assert.False(fornecedor.Situacao);
        }
    }
}
