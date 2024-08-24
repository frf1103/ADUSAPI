using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class CotacaoMoeda
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdMoeda { get; set; }
        public DateTime CotacaoData { get; set; }

        [Precision(18, 4)]
        public decimal CotacaoValor { get; set; }

        [ForeignKey("IdMoeda")]
        public Moeda moeda { get; set; }
    }
}