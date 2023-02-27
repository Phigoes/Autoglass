using Autoglass.Application.DTOs;
using Autoglass.Domain.Entities;
using AutoMapper;

namespace Autoglass.Application.Mapping
{
    public class DTOParaEntidadeMapping : Profile
    {
        public DTOParaEntidadeMapping()
        {
            CreateMap<FornecedorDTO, Fornecedor>();
            CreateMap<ProdutoDTO, Produto>();
        }
    }
}
