using System;

namespace Autoglass.Domain.Exceptions
{
    public class DataDeFabricacaoEhMaiorQueDataDeValidadeException : Exception
    {
        public DataDeFabricacaoEhMaiorQueDataDeValidadeException() : base("A data de fabricação é maior que a data de validade.")
        {
            
        }
    }
}
