using Microsoft.AspNetCore.Identity;

namespace BarberShop.Exceptions
{
    public class IdentityException(
        IdentityResult identityResult,
        string massage = "Identity exception"
        ) : System.Exception(massage) {
        public IdentityResult IdentityResult { get; init; } = identityResult
                    ?? throw new ArgumentNullException(nameof(identityResult)); 
    }
}
