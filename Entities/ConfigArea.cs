using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ConfigArea
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdSafra { get; set; }
        public int IdTalhao { get; set; }
        public int IdVariedade { get; set; }

        [Precision(18, 2)]
        public decimal Area { get; set; }

        public int? PopulacaoRecomendada { get; set; }

        [Precision(6, 2)]
        public decimal? Germinacao { get; set; }

        [Precision(18, 2)]
        public decimal? PMS { get; set; }

        [Precision(18, 2)]
        public decimal? Espacamento { get; set; }

        [Precision(6, 2)]
        public decimal? MargemSeguranca { get; set; }

        [Precision(18, 2)]
        public decimal? Stand { get; set; }

        [Precision(18, 2)]
        public decimal? UnidadeSementePrevista { get; set; }

        [Precision(18, 2)]
        public decimal? QtdSementePrevista { get; set; }

        [Precision(18, 2)]
        public decimal? ProdEstimada { get; set; }

        [ForeignKey("IdSafra,idconta")]
        public Safra safra { get; set; }

        [ForeignKey("IdTalhao,idconta")]
        public Talhao talhao { get; set; }

        [ForeignKey("IdVariedade")]
        public Variedade variedade { get; set; }

        public ICollection<PlanejamentoOperacao> planejamentoOperacao { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}