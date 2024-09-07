using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmPlannerAPI.Entities
{
    public class Unidade
    {
        public int id { get; set; }

        [MaxLength(20)]
        public string descricao { get; set; }

        [Precision(18, 4)]
        public decimal multiplo { get; set; }

        public ICollection<Produto> produtos { get; set; }
    }
}