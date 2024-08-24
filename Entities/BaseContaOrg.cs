using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace FarmPlannerAPI.Entities
{
    public class BaseContaOrg
    { 

        public int IdOrganizacao { get; set; }

        public Organizacao organizacao { get; set; }
    }
}
