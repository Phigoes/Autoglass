using Autoglass.Application.DTOs;
using Autoglass.Domain.Entities;
using AutoMapper;

namespace Autoglass.UnitTests.Helper
{
    public class TestMapper : Profile
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
                cfg.CreateMap<Produto, ProdutoDTO>().ReverseMap();
            });

            return config.CreateMapper();
        }
    }
}
