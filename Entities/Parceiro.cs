using ADUSAPI.Entities.Enum.ADUSAPI.Entities.Enum;
using ADUSAPICore.Models.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADUSAPI.Entities
{
    public class Parceiro
    {
        public string uid { get; set; }

        public string RazaoSocial { get; set; }
        public string Fantasia { get; set; }
        public TipodePessoa TipodePessoa { get; set; }
        public string Registro { get; set; }

        public string CEP { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }
        public string? Complemento { get; set; }

        public string Bairro { get; set; }
        public int UF { get; set; }
        public int Cidade { get; set; }
        public string? Profissao { get; set; }
        public TipoEstadoCivil EstadoCivil { get; set; }
        public string? IdRepresentante { get; set; }
        public Parceiro? Representante { get; set; }
        public DateTime DtNascimento { get; set; }

        public UF uf { get; set; }
        public Municipio cidade { get; set; }
        public ICollection<Parceiro>? empresas { get; set; }

        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }

        public string Fone1 { get; set; }
        public string? Fone2 { get; set; }
        public string? email { get; set; }
        public string? observacao { get; set; }
        public int Sexo { get; set; }
        public ICollection<Assinatura>? assinaturas { get; set; }
    }
}