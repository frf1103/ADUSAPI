using FarmPlannerAPI.Context;

using FarmPlannerAPICore.Models.Organizacao;
using FluentValidation;

namespace FarmPlannerAPI.Validators.Organizacao
{
    public class AdicionarOrganizacaoValidator : AbstractValidator<AdicionarOrganizacaoViewModel>
    {
        private readonly FarmPlannerContext _context;

        public AdicionarOrganizacaoValidator(FarmPlannerContext context)
        {
            _context = context;

            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("É necessário informar o Nome.")
                .MaximumLength(100).WithMessage("O nome da Organizacao deve ter no máximo 100 caracteres");

            RuleFor(c => c.Mascara)
                .NotEmpty().WithMessage("É necessário informar a Mascara da Orgnização")
                .MaximumLength(15).WithMessage("A Máscara deve ter no máximo 15 caracteres");
            RuleFor(c => c).Custom((organizacao, validateContext) =>
             {
                 var tipo = (int)organizacao.TipoPessoa;
                 var reg = organizacao.Registro;
                 if (FBSLIb.StringLib.ValidaRegistro(tipo, reg) == false)
                 {
                     validateContext.AddFailure("Registro Inválido");
                 }
                 var conta = context.organizacoes.Where(c => c.Registro == organizacao.Registro && c.Id != organizacao.Id).FirstOrDefault();
                 if (conta != null)
                 {
                     validateContext.AddFailure("CPF/CNPJ já existente para outra organização");
                 }
             });
        }

        /*   public bool ValidaRegistro(int tipo,string registro)
           {
               if (tipo == 0)
               {
                   string cpf = registro;
                   int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                   int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                   string tempCpf;
                   string digito;
                   int soma;
                   int resto;
                   cpf = cpf.Trim();
                   cpf = cpf.Replace(".", "").Replace("-", "");
                   if (cpf.Length != 11)
                       return false;
                   tempCpf = cpf.Substring(0, 9);
                   soma = 0;

                   for (int i = 0; i < 9; i++)
                       soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                   resto = soma % 11;
                   if (resto < 2)
                       resto = 0;
                   else
                       resto = 11 - resto;
                   digito = resto.ToString();
                   tempCpf = tempCpf + digito;
                   soma = 0;
                   for (int i = 0; i < 10; i++)
                       soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                   resto = soma % 11;
                   if (resto < 2)
                       resto = 0;
                   else
                       resto = 11 - resto;
                   digito = digito + resto.ToString();
                   return cpf.EndsWith(digito);
               }
               else
               {
                   string cnpj = registro;
                   int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                   int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
                   int soma;
                   int resto;
                   string digito;
                   string tempCnpj;
                   cnpj = cnpj.Trim();
                   cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
                   if (cnpj.Length != 14)
                       return false;
                   tempCnpj = cnpj.Substring(0, 12);
                   soma = 0;
                   for (int i = 0; i < 12; i++)
                       soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
                   resto = (soma % 11);
                   if (resto < 2)
                       resto = 0;
                   else
                       resto = 11 - resto;
                   digito = resto.ToString();
                   tempCnpj = tempCnpj + digito;
                   soma = 0;
                   for (int i = 0; i < 13; i++)
                       soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
                   resto = (soma % 11);
                   if (resto < 2)
                       resto = 0;
                   else
                       resto = 11 - resto;
                   digito = digito + resto.ToString();
                   return cnpj.EndsWith(digito);
               }
           }*/
    }
}