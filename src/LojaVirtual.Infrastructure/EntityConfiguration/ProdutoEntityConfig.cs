using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class ProdutoEntityConfig : IEntityTypeConfiguration<Produto>
{
    public void Configure(EntityTypeBuilder<Produto> b)
    {
        b.HasKey(p => p.Id);

        b.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        b.Property(p => p.Descricao)
            .IsRequired()
            .HasColumnType("varchar(700)");

        b.Property(p => p.Ativo)
            .IsRequired();

        b.Property(p => p.Valor)
            .IsRequired();

        b.Property(p => p.Cadastro)
            .IsRequired();

        b.Property(p => p.QuantidadeEstoque)
            .IsRequired();

        b.HasOne(p => p.Categoria)
            .WithMany(c => c.Produtos);
    }
}