using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class GrupoConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdOrganizacao { get; set; }
        public int IdClasseConta { get; set; }
        public string? CodigoCliente { get; set; }
        public string? CodigoExterno { get; set; }

        [ForeignKey("IdOrganizacao,idconta")]
        public Organizacao organizacao { get; set; }

        [ForeignKey("IdClasseConta")]
        public ClasseConta classeConta { get; set; }

        public ICollection<CadastroConta> cadastroContas { get; set; }
        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}