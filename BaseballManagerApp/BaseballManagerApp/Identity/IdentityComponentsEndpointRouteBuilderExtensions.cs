using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BaseballManagerApp.Identity;

internal static class IdentityEndpointRouteBuilderExtensions
{
    public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var accountGroup = endpoints.MapGroup("/Account");
        accountGroup.MapPost("/Logout", async (
            ClaimsPrincipal user,
            SignInManager<ApplicationUser> signInManager) =>
        {
            await signInManager.SignOutAsync();
            return TypedResults.LocalRedirect("/"); //redirect to home page
        });

        return accountGroup;
    }
}
