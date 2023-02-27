using Autoglass.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Autoglass.Infrastructure.Persistence.Configurations
{
    public class ProdutoConfigurations : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(255);

            builder.Property(p => p.Situacao).IsRequired();

            builder.Property(p => p.DataDeFabricacao).IsRequired();

            builder.Property(p => p.DataDeValidade).IsRequired();

            builder.HasOne(p => p.Fornecedor)
                .WithMany(p => p.Produtos)
                .HasForeignKey(p => p.IdFornecedor)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
