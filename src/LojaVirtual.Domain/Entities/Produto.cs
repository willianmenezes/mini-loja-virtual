using LojaVirtual.Core.Domain;

namespace LojaVirtual.Domain.Entities;

public class Produto : Entity
{
    public Guid CategoriaId { get; private set; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }
    public bool Ativo { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Cadastro { get; private set; }
    public int QuantidadeEstoque { get; private set; }

    // Relações Entity
    public Categoria? Categoria { get; set; }

    public Produto(string nome, string descricao, decimal valor, int quantidadeEstoque, Guid categoriaId)
    {
        Nome = nome;
        Descricao = descricao;
        Valor = valor;
        QuantidadeEstoque = quantidadeEstoque;
        CategoriaId = categoriaId;
        Ativo = true;
        Cadastro = DateTime.UtcNow;
        
        Validar();
    }

    public void Ativar() => Ativo = true;

    public void Desativar() => Ativo = false;

    public void AlterarNome(string novoNome)
    {
        if (string.IsNullOrWhiteSpace(novoNome))
            throw new DomainException("Nome inválido.");

        Nome = novoNome;
    }

    public void AlterarDescricao(string novaDescricao)
    {
        if (string.IsNullOrWhiteSpace(novaDescricao))
            throw new DomainException("Descricao inválida.");

        Descricao = novaDescricao;
    }

    public void DebitarEstoque(int quantidade)
    {
        if (quantidade < 0)
            throw new DomainException("Quantidade invalida.");

        if (!PossuiEstoque(quantidade))
        {
            throw new DomainException("Quantidade em estoque insuficiente");
        }

        QuantidadeEstoque -= quantidade;
    }

    public void ReporEstoque(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade inválida.");

        QuantidadeEstoque += quantidade;
    }
    
    public void AlterarValor(decimal valor)
    {
        if (valor is <= 0 or >= decimal.MaxValue)
            throw new DomainException("Valor inválido.");

        Valor = valor;
    }
    
    public bool PossuiCategoria(Guid categoriaId)
    {
        return CategoriaId == categoriaId;
    }
    
    public void AlterarCategoria(Guid categoria)
    {
        
    }

    public bool PossuiEstoque(int quantidade) => QuantidadeEstoque >= quantidade;


    public sealed override void Validar()
    {
        if (string.IsNullOrWhiteSpace(Nome))
            throw new DomainException("Produto inválido");

        if (string.IsNullOrWhiteSpace(Descricao))
            throw new DomainException("Produto inválido");

        if (Valor <= 0)
            throw new DomainException("Produto inválido");

        if (QuantidadeEstoque < 0)
            throw new DomainException("Produto inválido");

        if (CategoriaId == Guid.Empty)
            throw new DomainException("Categoria inválida");
    }
}