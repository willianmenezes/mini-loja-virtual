using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LojaVirtual.Infrastructure.EntityConfiguration;

public class PagamentoEntityConfig : IEntityTypeConfiguration<Pagamento>
{
    public void Configure(EntityTypeBuilder<Pagamento> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Valor)
            .IsRequired();
        
        builder.Property(x => x.PedidoId)
            .IsRequired();
        
        builder.Property(x => x.ExpiracaoCartaoCredito)
            .IsRequired();
        
        builder.Property(x => x.NomeCartaoCredito)
            .IsRequired();
        
        builder.Property(x => x.NumeroCartaoCredito)
            .IsRequired();
        
        builder.Property(x => x.DigitoVerificadorCartaoCredito)
            .IsRequired();
        
        builder.Property(x => x.Cadastro)
            .IsRequired();

        builder.HasOne(x => x.Transacao)
            .WithOne(x => x.Pagamento);

        builder.HasOne(x => x.Pedido)
            .WithOne(x => x.Pagamento);
    }
}