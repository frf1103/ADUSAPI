using FarmPlannerAPI.Entities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ClasseConta
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int TipoClasseConta { get; set; }
        public ICollection<GrupoConta> grupoContas { get; set; }
    }
}