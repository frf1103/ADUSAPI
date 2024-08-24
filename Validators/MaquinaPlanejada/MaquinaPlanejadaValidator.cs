using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.MaquinaPlanejada;
using FluentValidation;

namespace FarmPlannerAPI.Validators.MaquinaPlanejada
{
    public class MaquinaPlanejadaValidator : AbstractValidator<MaquinaPlanejadaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public MaquinaPlanejadaValidator(FarmPlannerContext context)
        {
            _context = context;

            /*            RuleFor(c => c.Descricao)
                .NotEmpty().WithMessage("É necessário informar a Descricao.")
                .MaximumLength(100).WithMessage("A Descriçao deve ter no máximo 100 caracteres");
            */
        }
    }

    public class ExcluirMaquinaPlanejadaValidator : AbstractValidator<MaquinaPlanejadaViewModel>
    {
        private readonly FarmPlannerContext _context;
        public ExcluirMaquinaPlanejadaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((orc, validateContext) =>
            {
                /*                var conf = _context.produtosorcamento.Where(c => c.IdOrcamento == orc.Id).FirstOrDefault();
                                if (conf != null)
                                {
                                    validateContext.AddFailure("Existem produtos para esse orçamento");
                                }

                                var conf1 = _context.planejoperacoes.Where(c => c.IdOrcamento == orc.Id).FirstOrDefault();
                                if (conf1 != null)
                                {
                                    validateContext.AddFailure("Orçamento com planejamento");
                                }
                */
            });

        }


    }

}
