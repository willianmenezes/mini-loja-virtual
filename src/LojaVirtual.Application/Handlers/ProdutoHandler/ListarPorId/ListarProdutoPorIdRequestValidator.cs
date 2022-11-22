using FluentValidation;

namespace LojaVirtual.Application.Handlers.ProdutoHandler.ListarPorId;

public class ListarProdutoPorIdRequestValidator : AbstractValidator<ListarProdutoPorIdRequest>
{
    public ListarProdutoPorIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O id é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("Id inválido.");
    }
}