using System.Threading.Tasks;
using CurrencyData.Infrastructure.Domain;

namespace CurrencyData.Infrastructure.Services.Abstractions
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Authenticate(User user);
    }
}
