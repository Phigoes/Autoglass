using Autoglass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoglass.Infrastructure.Persistence.Configurations
{
    public class FornecedorConfigurations : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Descricao).IsRequired().HasMaxLength(255);

            builder.Property(p => p.Situacao).IsRequired();

            builder.Property(f => f.CNPJ).IsRequired();
        }
    }
}
