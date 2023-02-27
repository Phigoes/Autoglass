using System;

namespace Autoglass.Domain.Entities.Common.Helper
{
    public class FiltroProduto
    {
        public string Descricao { get; set; }
        public bool Situacao { get; set; }
        public DateTime? DataDeFabricacao { get; set; }
        public DateTime? DataDeValidade { get; set; }
        public int? Pagina { get; set; }
        public int? TamanhoPagina { get; set; }
    }
}
