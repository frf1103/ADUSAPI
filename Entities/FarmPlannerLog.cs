using Microsoft.Extensions.Configuration.UserSecrets;

namespace FarmPlannerAPI.Entities
{
    public class FarmPlannerLog
    {
        public int id { get; set; }
        public string uid { get; set; }
        public DateTime datalog { get; set; }
        public string transacao { get; set; }
    }
}