using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.Safra;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Safra
{
    public class SafraValidator:AbstractValidator<SafraViewModel>
    {
        private readonly FarmPlannerContext _context;
        public SafraValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao ter no máximo 100 caracteres");

            RuleFor(c => c.DataInicio)
                .NotEmpty().WithMessage("É necessário informar a data inicio");
            RuleFor(c => c.DataFim)
                .NotEmpty().WithMessage("É necessário informar a data fim");
/*            RuleFor(c => c.IdCultura && (!c.Abertura && !c.Reforma)
                .NotEmpty().NotEqual(0)
                .WithMessage("É necessário informar a cultura");
*/
            RuleFor(c => c).Custom((anoagricola, validateContext) =>
            {

                if (anoagricola.DataFim < anoagricola.DataInicio)
                {
                    validateContext.AddFailure("Data fim não pode ser menor que data inicio");
                }
                if (!anoagricola.Reforma && !anoagricola.Abertura && anoagricola.IdCultura.GetValueOrDefault(0)==0)
                {
                    validateContext.AddFailure("Cultura deve ser informada");
                }

            });

        }


    }

    public class ExcluirSafraValidator : AbstractValidator<SafraViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirSafraValidator(FarmPlannerContext context)
        {
            _context = context;


            RuleFor(c => c).Custom((safra, validateContext) =>
            {
                var conf = _context.configareas.Where(c => c.IdSafra == safra.Id).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Existem configuraçoes para essa safra");
                }
                var conf1 = _context.orcamentosproduto.Where(c => c.IdSafra == safra.Id).FirstOrDefault();
                if (conf1 != null)
                {
                    validateContext.AddFailure("Existem orçamentos para essa safra");
                }
                var conf2 = _context.planejamentoCompras.Where(c => c.IdSafra == safra.Id).FirstOrDefault();
                if (conf2 != null)
                {
                    validateContext.AddFailure("Existem planejamentos de compra para essa safra");
                }
            });


        }


    }

}
