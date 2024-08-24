using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class EntregaContrato
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int IdComercializacao { get; set; }
        public DateTime DataEntrega { get; set; }

        [MaxLength(100)]
        public string Documento { get; set; }

        [Precision(18, 2)]
        public Decimal Quantidade { get; set; }

        [ForeignKey("IdComercializacao,idconta")]
        public Comercializacao comercializacao { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}