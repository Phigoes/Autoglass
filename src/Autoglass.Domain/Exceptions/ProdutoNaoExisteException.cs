using System;

namespace Autoglass.Domain.Exceptions
{
    public class ProdutoNaoExisteException : Exception
    {
        public ProdutoNaoExisteException() : base("Produto não existe")
        {
            
        }
    }
}
