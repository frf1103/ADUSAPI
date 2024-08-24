using FarmPlannerAPI.Context;
using FluentValidation.AspNetCore;
using FarmPlannerAPICore.Models.AnoAgricola;
using FarmPlannerAPICore.Models.Conta;
using FluentValidation;

namespace FarmPlannerAPI.Validators.AnoAgricola
{
    public class AdicionarAnoAgricolaValidator : AbstractValidator<AdicionarAnoAgricolaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public AdicionarAnoAgricolaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao ter no máximo 100 caracteres");

            RuleFor(c => c.DataInicio)
                .NotEmpty().WithMessage("É necessário informar a data inicio");
            RuleFor(c => c.DataFim)
                .NotEmpty().WithMessage("É necessário informar a data fim");

            RuleFor(c => c).Custom((anoagricola, validateContext) =>
            {

                if (anoagricola.DataFim<anoagricola.DataInicio)
                {
                    validateContext.AddFailure("Data fim não pode ser menor que data inicio");
                }
            });

        }

    }
}