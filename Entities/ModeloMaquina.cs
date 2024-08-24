using FarmPlannerAPI.Entities.Enum;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class ModeloMaquina
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public int IdMarca { get; set; }

        [ForeignKey("IdMarca")]
        public MarcaMaquina marcaMaquina { get; set; }

        public int Combustivel { get; set; }

        public ICollection<ModeloParametro>? modelosparametros { get; set; }
        public ICollection<Maquina>? maquinas { get; set; }

        public ICollection<MaquinaPlanejada> maquinasplanejada { get; set; }
    }
}