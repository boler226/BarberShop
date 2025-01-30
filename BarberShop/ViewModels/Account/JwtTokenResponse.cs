 namespace BarberShop.ViewModels.Account
{
    public class JwtTokenResponse {
        public bool IsLogedIn { get; set; } = false; 
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
    }
}
