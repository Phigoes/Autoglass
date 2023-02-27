using System;

namespace Autoglass.Domain.Entities
{
    public class Produto : EntidadeBase
    {
        public Produto() { }

        public Produto(string descricao, bool situacao, DateTime dataDeFabricacao, DateTime dataDeValidade, int idFornecedor)
        {
            Descricao = descricao;
            Situacao = situacao;
            DataDeFabricacao = dataDeFabricacao;
            DataDeValidade = dataDeValidade;
            IdFornecedor = idFornecedor;
        }

        public string Descricao { get; private set; }
        public bool Situacao { get; private set; }
        public DateTime DataDeFabricacao { get; private set; }
        public DateTime DataDeValidade { get; private set; }
        public int IdFornecedor { get; private set; }
        public Fornecedor Fornecedor { get; private set; }

        public void Inativar()
        {
            Situacao = false;
        }

        public bool CompararDataDeFabricacaoComDataDeValidade()
        {
            return DataDeValidade > DataDeFabricacao;
        }
    }
}
