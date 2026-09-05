using FluentValidation;

namespace Tareas.Application.UseCases.Tareas.Commands.DeleteTarea;

public class DeleteTareaValidator : AbstractValidator<DeleteTareaCommand>
{
    
    public DeleteTareaValidator()
    {
        
        RuleFor(x => x.Id).NotEmpty().WithMessage("El id es obligatorio.");
    }
}