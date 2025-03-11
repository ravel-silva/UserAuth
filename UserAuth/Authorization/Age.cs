using Microsoft.AspNetCore.Authorization;

namespace UserAuth.Authorization
{
    public class Age : IAuthorizationRequirement
    {
        public Age (int idade)
        {
            idadeMinima = idade;
        }
        public int idadeMinima { get; set; }
    }
}
