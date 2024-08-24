using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class Cultura
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }
        public string UnidadeProdutiva { get; set; }
        public string NomeProduto { get; set; }
        public int DiasEstimadosEmergencia { get; set; }
        public string? CodigoExterno { get; set; }
        public ICollection<Safra> Safras { get; set; }
        public ICollection<Variedade> Variedades { get; set; }
        public ICollection<Fazenda>? fazendas { get; set; }
        public ICollection<ModeloParametro>? modelosparametro { get; set; }

        public ICollection<MaquinaParametro>? maquinasparametro { get; set; }
    }
}