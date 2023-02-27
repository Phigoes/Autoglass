using System.Collections;
using System.Collections.Generic;

namespace Autoglass.Domain.Entities
{
    public class Fornecedor : EntidadeBase
    {
        public Fornecedor() { }

        public Fornecedor(string descricao, bool situacao, string cnpj)
        {
            Descricao = descricao;
            Situacao = situacao;
            CNPJ = cnpj;

            Produtos = new List<Produto>();
        }

        public string Descricao { get; private set; }
        public bool Situacao { get; set; }
        public string CNPJ { get; private set; }
        public List<Produto> Produtos { get; private set; }

        public void Inativar()
        {
            Situacao = false;
        }
    }
}
