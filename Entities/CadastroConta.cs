using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class CadastroConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdGrupoConta { get; set; }
        public string? CodigoExterno { get; set; }
        public string? CodigoCliente { get; set; }

        [ForeignKey("IdGrupoConta")]
        public GrupoConta grupoConta { get; set; }

        public ICollection<OrcamentoCustoIndireto> orcamentoCustoIndiretos { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }
    }
}