using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class PedidoItemEntityConfig : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> b)
    {
        b.HasKey(i => i.Id);

        b.Property(i => i.Quantidade)
            .IsRequired();
        
        b.Property(i => i.PedidoId)
            .IsRequired();
        
        b.Property(i => i.NomeProduto)
            .IsRequired()
            .HasColumnType("varchar(200)");
        
        b.Property(i => i.ProdutoId)
            .IsRequired();
        
        b.Property(i => i.ValorUnitario)
            .IsRequired();
        
        b.Property(i => i.Cadastro)
            .IsRequired();
        
        b.HasOne(p => p.Pedido)
            .WithMany(c => c.PedidoItens);
    }
}