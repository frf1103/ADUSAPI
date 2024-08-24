using FarmPlannerAPI.Context;

using FarmPlannerAPICore.Models.CustosIndiretos;
using FluentValidation;

namespace FarmPlannerAPI.Validators.CustosIndiretos
{
    public class ClasseContaValidator : AbstractValidator<ClasseContaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ClasseContaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirClasseContaValidator : AbstractValidator<ClasseContaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirClasseContaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {

                var princ = context.gruposcontas.FirstOrDefault(c => c.IdClasseConta == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem grupos de conta vinculadas a essa classe");
                }

            });
        }

    }
}
