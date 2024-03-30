using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace TastyDelivery.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string FindId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
