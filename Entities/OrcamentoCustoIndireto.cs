using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class OrcamentoCustoIndireto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdSafra { get; set; }
        public DateTime Data { get; set; }
        public int idcontaCad { get; set; }

        [Precision(18, 2)]
        public Decimal valor { get; set; }

        [ForeignKey("idcontaCad,idconta")]
        public CadastroConta contacad { get; set; }

        [ForeignKey("IdSafra,idconta")]
        public Safra safra { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}