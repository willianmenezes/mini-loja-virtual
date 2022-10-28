using AutoMapper;
using FluentValidation;
using LojaVirtual.Core.NotificationError;
using MediatR;

namespace LojaVirtual.Application.Handlers;

public abstract class BaseHandler
{
    protected readonly IMediator Mediator;
    protected readonly IMapper Mapper;

    protected BaseHandler(IMediator mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    protected bool Validar<TEntity, TValidator>(TEntity entity, TValidator validator) where TEntity : class
        where TValidator : AbstractValidator<TEntity>
    {
        var resultadoValidacao = validator.Validate(entity);

        foreach (var erro in resultadoValidacao.Errors)
        {
            Mediator.Publish(new NotificacaoErro(entity.GetType().Name, erro.ErrorMessage));
        }

        return resultadoValidacao.IsValid;
    }
}