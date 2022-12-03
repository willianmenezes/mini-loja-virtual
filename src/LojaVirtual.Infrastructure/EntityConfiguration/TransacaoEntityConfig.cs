using LojaVirtual.Domain.Entities;
using LojaVirtual.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class TransacaoEntityConfig : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(t => t.Status)
            .HasConversion(t => t.ToString(), 
                t => (StatusTransacao)Enum.Parse(typeof(StatusTransacao), t))
            .HasColumnType("varchar(50)")
            .IsRequired();

        builder.Property(x => x.Total)
            .IsRequired();
        
        builder.Property(x => x.PagamentoId)
            .IsRequired();
        
        builder.Property(x => x.PagamentoGatewayId)
            .HasColumnType("varchar(100)")
            .IsRequired();
        
        builder.Property(x => x.Cadastro)
            .IsRequired();

        builder.HasOne(x => x.Pagamento)
            .WithOne(c => c.Transacao);
    }
}