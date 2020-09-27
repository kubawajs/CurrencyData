using System.Threading.Tasks;
using CurrencyData.Infrastructure.Domain;

namespace CurrencyData.Infrastructure.Repositories.Abstractions
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(User user);
    }
}
