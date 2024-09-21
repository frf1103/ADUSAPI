using FarmPlannerAPI.Entities.Enum;
using FarmPlannerAPI.Entities.Enum.FarmPlannerAPI.Entities.Enum;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class Fazenda
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdOrganizacao { get; set; }
        public string Descricao { get; set; }
        public int IdUF { get; set; }
        public int IdMunicipio { get; set; }
        public int Tipo { get; set; }
        public int TipoArrenda { get; set; }

        [Precision(18, 2)]
        public decimal ValorArrendamento { get; set; }

        public string? CodigoExterno { get; set; }
        public int IdRegiao { get; set; }
        public int? IdCultura { get; set; }
        public UF uF { get; set; }
        public Municipio municipio { get; set; }

        public ICollection<Talhao> Talhoes { get; set; }

        public Cultura? cultura { get; set; }

        public Regiao regiao { get; set; }

        [ForeignKey("IdOrganizacao,idconta")]
        public Organizacao organizacao { get; set; }

        public ICollection<OrcamentoProduto> OrcamentoProduto { get; set; }

        public ICollection<PlanejamentoCompra> planejamentos { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}