using FluentValidation;

namespace LojaVirtual.Application.Handlers.PedidoHandler.AdicionarItemPedido;

public class AdicionarPedidoRequestValidator : AbstractValidator<AdicionarItemPedidoRequest>
{
    public AdicionarPedidoRequestValidator()
    {
        RuleFor(x => x.ProdutoId)
            .NotEqual(Guid.Empty).WithMessage("Produto inválido.");

        RuleFor(x => x.UsuarioId)
            .NotEqual(Guid.Empty).WithMessage("Usuário inválido");

        RuleFor(x => x.Quantidade)
            .GreaterThan(0).WithMessage("Quantidade inválida");
    }
}