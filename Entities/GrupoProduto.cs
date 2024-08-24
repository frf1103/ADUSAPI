using FarmPlannerAPI.Entities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace FarmPlannerAPI.Entities
{
    public class GrupoProduto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Descricao { get; set; }

        public int Tipo { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}