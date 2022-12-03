using LojaVirtual.Core.Data;
using LojaVirtual.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LojaVirtual.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IUnityOfWork
{
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItem { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(x => Console.WriteLine(x)).EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<string>().AreUnicode(false).HaveColumnType("varchar(400)");
        base.ConfigureConventions(configurationBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public async Task SalvarAlteracoesAsync()
    {
        foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("Cadastro") != null))
        {
            if (entry.State == EntityState.Modified)
                entry.Property("Cadastro").IsModified = false;
        }

        await SaveChangesAsync();
    }
}