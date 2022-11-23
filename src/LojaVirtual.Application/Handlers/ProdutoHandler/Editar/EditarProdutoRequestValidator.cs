using FluentValidation;

namespace LojaVirtual.Application.Handlers.ProdutoHandler.Editar;

public class EditarProdutoRequestValidator : AbstractValidator<EditarProdutoRequest>
{
    public EditarProdutoRequestValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O nome da produto é obrigatório.")
            .MinimumLength(3).WithMessage("O nome do produto deve ter mais que {MinLength} caracteres.")
            .MaximumLength(200).WithMessage("O nome do produto deve ter menos que {MaxLength} caracteres.");
        
        RuleFor(p => p.Descricao)
            .NotEmpty().WithMessage("A descrição do produto e obrigatória.")
            .MinimumLength(3).WithMessage("A descrição do produto deve ter mais que {MinLength} caracteres.")
            .MaximumLength(700).WithMessage("A descrição do produto deve ter menos que {MaxLength} caracteres.");

        RuleFor(p => p.Valor)
            .GreaterThan(0).WithMessage("O valor do produto deve ser maior que {PropertyValue}.")
            .LessThan(decimal.MaxValue).WithMessage("O valor do produto deve ser menor que {PropertyValue}.");
        
        RuleFor(p => p.CategoriaId)
            .NotEqual(Guid.Empty).WithMessage("Categoria inválida."); 
        
        RuleFor(p => p.Id)
            .NotEqual(Guid.Empty).WithMessage("Produto inválido.");
    }
}