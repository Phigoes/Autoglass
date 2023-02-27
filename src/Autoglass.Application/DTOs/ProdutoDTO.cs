using System;

namespace Autoglass.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataDeFabricacao { get; set; }
        public DateTime DataDeValidade { get; set; }
        public int IdFornecedor { get; set; }
    }
}
