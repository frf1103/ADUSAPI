using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class MaquinaParametro
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdMaquina { get; set; }
        public int IdCultura { get; set; }
        public int IdOperacao { get; set; }

        [Precision(18, 2)]
        public decimal Rendimento { get; set; }

        [Precision(18, 2)]
        public decimal Consumo { get; set; }

        [ForeignKey("IdCultura")]
        public Cultura cultura { get; set; }

        [ForeignKey("IdOperacao,idconta")]
        public Operacao operacao { get; set; }

        [ForeignKey("IdMaquina,idconta")]
        public Maquina maquina { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}