using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.CustosIndiretos;

using FluentValidation;

namespace FarmPlannerAPI.Validators.CustosIndiretos
{
    public class GrupoContaValidator : AbstractValidator<GrupoContaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public GrupoContaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descricao deve ter no máximo 100 caracteres");

        }

    }

    public class ExcluirGrupoContaValidator : AbstractValidator<GrupoContaViewModel>
    {

        private readonly FarmPlannerContext _context;
        public ExcluirGrupoContaValidator(FarmPlannerContext context)
        {
            RuleFor(c => c).Custom((tipooper, validateContext) =>
            {

                var princ = context.cadastrocontas.FirstOrDefault(c => c.IdGrupoConta == tipooper.Id);
                if (princ != null)
                {
                    validateContext.AddFailure("Existem contas vinculas a esse grupo");
                }

            });
        }

    }

}
