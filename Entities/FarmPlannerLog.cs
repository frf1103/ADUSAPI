using Microsoft.Extensions.Configuration.UserSecrets;

namespace ADUSAPI.Entities
{
    public class ADUSLog
    {
        public string idconta { get; set; }
        public int id { get; set; }
        public string uid { get; set; }
        public DateTime datalog { get; set; }
        public string transacao { get; set; }
    }
}