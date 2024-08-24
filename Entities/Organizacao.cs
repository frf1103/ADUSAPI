using FarmPlannerAPI.Entities.Enum.FarmPlannerAPI.Entities.Enum;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Organizacao
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nome { get; set; }

        [MaxLength(15)]
        public string Mascara { get; set; }

        public TipodePessoa TipoPessoa { get; set; }
        public string Registro { get; set; }

        public ICollection<Fazenda> fazendas { get; set; }
        public ICollection<AnoAgricola> anosagricolas { get; set; }
        public ICollection<GrupoConta> grupoContas { get; set; }

        public string idconta { get; set; }

        [ForeignKey("idconta")]
        public Conta conta { get; set; }

        public ICollection<OrganizacaoUsuario> organizacaoUsuarios { get; set; }

        public ICollection<PreferUsu> preferUsus { get; set; }

        public ICollection<Maquina> maquinas { get; set; }
        public string? uid { get; set; }
        public DateTime? datains { get; set; }
        public DateTime? dataup { get; set; }
    }
}