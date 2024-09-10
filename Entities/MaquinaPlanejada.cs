using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace FarmPlannerAPI.Entities
{
    public class MaquinaPlanejada
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdPlanejamento { get; set; }
        public int IdModeloMaquina { get; set; }
        public int? IdMaquina { get; set; }

        [Precision(18, 2)]
        public decimal Rendimento { get; set; }

        [Precision(18, 2)]
        public decimal Consumo { get; set; }

        [Precision(18, 2)]
        public decimal QtdHoraEstimada { get; set; }

        [Precision(18, 2)]
        public decimal QtdCombEstimado { get; set; }

        [ForeignKey("IdMaquina,idconta")]
        public Maquina? maquina { get; set; }

        [ForeignKey("IdModeloMaquina,idconta")]
        public ModeloMaquina modelomaquina { get; set; }

        [ForeignKey("IdPlanejamento,idconta")]
        public PlanejamentoOperacao planejamentoOperacao { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}