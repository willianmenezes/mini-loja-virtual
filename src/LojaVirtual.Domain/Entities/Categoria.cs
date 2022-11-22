using LojaVirtual.Core.Domain;

namespace LojaVirtual.Domain.Entities;

public class Categoria : Entity
{
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public DateTime Cadastro { get; }

    // Relações Entity
    public IEnumerable<Produto> Produtos { get; set; }

    public Categoria(string nome, string descricao)
    {
        Nome = nome;
        Descricao = descricao;
        Ativo = true;
        Cadastro = DateTime.UtcNow;

        Validar();
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new DomainException("Categoria inválida.");

        Nome = novoNome;
    }

    public void AlterarDescricao(string novaDescricao)
    {
        if (string.IsNullOrWhiteSpace(novaDescricao))
            throw new DomainException("Descricao inválida.");

        Descricao = novaDescricao;
    }

    public sealed override void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new DomainException("Categoria inválida");

        if (string.IsNullOrWhiteSpace(Descricao))
            throw new DomainException("Categoria inválida");
    }
}