using FarmPlannerAPI.Context;
using FarmPlannerAPICore.Models.ConfigArea;
using FluentValidation;

namespace FarmPlannerAPI.Validators.ConfigArea
{
    public class AdicionarConfigAreaValidator : AbstractValidator<ConfigAreaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public AdicionarConfigAreaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Area)
                .NotEqual(0).NotEmpty().WithMessage("É necessário informar uma área");
            /*   RuleFor(c => c.Espacamento)
               .NotEmpty().WithMessage("É necessário informar Espaçamento");
               RuleFor(c => c.PopulacaoRecomendada)
               .NotEmpty().WithMessage("É necessário informar População recomendada");
               RuleFor(c => c.Stand)
               .NotEmpty().WithMessage("É necessário informar Stand");
               RuleFor(c => c.Germinacao)
               .NotEmpty().WithMessage("É necessário informar Germinação");
               RuleFor(c => c.MargemSeguranca)
               .NotEmpty().WithMessage("É necessário informar Margem de Segurança");
               RuleFor(c => c.PMS)
               .NotEmpty().WithMessage("É necessário informar PMS");
               RuleFor(c => c.ProdEstimada)
               .NotEmpty().WithMessage("É necessário informar Prod Estimada");
               RuleFor(c => c.QtdSementePrevista)
               .NotEmpty().WithMessage("É necessário informar Qtd Semente prevista");
               RuleFor(c => c.UnidadeSementePrevista)
               .NotEmpty().WithMessage("É necessário informar Unidade Semente prevista");
            */
            RuleFor(c => c).Custom((ConfigArea, validateContext) =>
            {
                var conf = _context.configareas.Where(c => c.IdSafra == ConfigArea.IdSafra
                && c.IdTalhao == ConfigArea.IdTalhao && c.IdVariedade == ConfigArea.IdVariedade).FirstOrDefault();
                if (conf != null)
                {
                    validateContext.AddFailure("Já existe área configurada para esse talhão/safra/variedade");
                }
                var soma = _context.configareas.Where(c => c.IdTalhao == ConfigArea.IdTalhao && c.IdSafra == ConfigArea.IdSafra).Sum(c => c.Area);
                var t = _context.talhoes.Where(c => c.Id == ConfigArea.IdTalhao).FirstOrDefault();

                if (soma + ConfigArea.Area > t.AreaProdutiva)
                {
                    validateContext.AddFailure("Soma das áreas configuradas deverá ser menor ou igual a área produtiva do talhão para a mesma safra");
                }
            });
        }
    }

    public class ExcluirConfigAreaValidator : AbstractValidator<ConfigAreaViewModel>
    {
        private readonly FarmPlannerContext _context;

        public ExcluirConfigAreaValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c).Custom((ConfigArea, validateContext) =>
            {
                var conf1 = _context.planejoperacoes.FirstOrDefault(x => x.IdConfigArea == ConfigArea.Id);
                if (conf1 != null)

                {
                    validateContext.AddFailure("Configuração com Planejamento de operações");
                }
            });
        }
    }
}