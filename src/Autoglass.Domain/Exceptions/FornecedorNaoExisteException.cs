using System;

namespace Autoglass.Domain.Exceptions
{
    public class FornecedorNaoExisteException : Exception
    {
        public FornecedorNaoExisteException() : base("Fornecedor não existe")
        {
            
        }
    }
}
