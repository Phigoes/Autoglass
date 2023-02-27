using Autoglass.Application.DTOs;
using Autoglass.Domain.Entities;
using AutoMapper;

namespace Autoglass.Application.Mapping
{
    public class EntidadeParaDTOMapping : Profile
    {
        public EntidadeParaDTOMapping()
        {
            CreateMap<Fornecedor, FornecedorDTO>();
            CreateMap<Produto, ProdutoDTO>();
        }
    }
}
