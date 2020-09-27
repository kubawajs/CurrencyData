using CurrencyData.Infrastructure.Domain;
using CurrencyData.Infrastructure.Repositories.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Repositories.Mocks
{
    public class StaticUserRepository : IUserRepository
    {
        private readonly IEnumerable<User> _appUsers = new List<User>
        {
            new User {Username = "admin", Password = "12345"}
        };

        public async Task<User> GetUserAsync(User user) =>
            await Task.FromResult(_appUsers.SingleOrDefault(x => x.Username == user.Username && x.Password == user.Password));
    }
}
