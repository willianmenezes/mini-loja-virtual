namespace LojaVirtual.Core.Domain;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime Cadastro { get; private set; }
    
    protected Entity()
    {
        Id = Guid.NewGuid();
        Cadastro = DateTime.UtcNow;
    }
    
    public abstract void Validar();
}