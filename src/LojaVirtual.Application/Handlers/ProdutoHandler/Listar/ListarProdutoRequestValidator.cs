using FluentValidation;

namespace LojaVirtual.Application.Handlers.ProdutoHandler.Listar;

public class ListarProdutoRequestValidator : AbstractValidator<ListarProdutoRequest>
{
    public ListarProdutoRequestValidator()
    {
        RuleFor(p => p.Indice)
            .GreaterThan(0).WithMessage("O índice da página deve ser maior que {PropertyValue}.");

        RuleFor(p => p.TamanhoPagina)
            .GreaterThan(0).WithMessage("O tamanho da página deve ser maior que {PropertyValue}")
            .LessThan(100).WithMessage("O tamanho da página deve ser menor que {PropertyValue}");
    }
}