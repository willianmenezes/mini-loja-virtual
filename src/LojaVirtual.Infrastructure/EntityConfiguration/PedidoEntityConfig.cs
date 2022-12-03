using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class PedidoEntityConfig : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> b)
    {
        b.HasKey(p => p.Id);

        b.Property(p => p.Status)
            .HasConversion(p => p.ToString(), 
                p => (StatusPedido)Enum.Parse(typeof(StatusPedido), p))
            .HasColumnType("varchar(50)")
            .IsRequired();

        b.Property(p => p.UsuarioId)
            .IsRequired();

        b.Property(p => p.ValorTotal)
            .IsRequired();
        
        b.Property(p => p.Cadastro)
            .IsRequired();
        
        b.HasMany(p => p.PedidoItens)
            .WithOne(i => i.Pedido)
            .HasForeignKey(p => p.PedidoId)
            .HasConstraintName("PedidoItem_Pedido_FK");
        
        b.HasOne(x => x.Pagamento)
            .WithOne(x => x.Pedido);
    }
}