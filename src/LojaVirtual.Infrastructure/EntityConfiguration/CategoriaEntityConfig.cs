using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class CategoriaEntityConfig : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> b)
    {
        b.HasKey(c => c.Id);
        
        b.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        b.Property(p => p.Descricao)
            .IsRequired()
            .HasColumnType("varchar(700)");

        b.Property(p => p.Ativo)
            .IsRequired();
        
        b.Property(p => p.Cadastro)
            .IsRequired();

        b.HasMany(c => c.Produtos)
            .WithOne(p => p.Categoria)
            .HasForeignKey(p => p.CategoriaId)
            .HasConstraintName("Produto_Categoria_FK");

    }
}