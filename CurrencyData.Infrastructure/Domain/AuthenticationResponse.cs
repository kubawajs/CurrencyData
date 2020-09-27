namespace CurrencyData.Infrastructure.Domain
{
    public class AuthenticationResponse
    {
        public bool Result { get; set; }
        public User User { get; set; }
        public string Token { get; set; }
    }
}
