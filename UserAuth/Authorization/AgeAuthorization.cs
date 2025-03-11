using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace UserAuth.Authorization
{
    public class AgeAuthorization : AuthorizationHandler<Age>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Age requirement)
        {
            var dataNascimentoClaim = context.User.FindFirst("dateOfBirth");

            if (dataNascimentoClaim == null)
            {
                return Task.CompletedTask;
            }

            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);
            var idade = DateTime.Today.Year - dataNascimento.Year;
            if (dataNascimento > DateTime.Today.AddYears(-idade))
            {
                idade--;
            }
            if (idade >= requirement.idadeMinima)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
