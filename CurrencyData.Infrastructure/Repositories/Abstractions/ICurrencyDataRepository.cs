using System.Threading.Tasks;
using CurrencyData.Infrastructure.Domain;

namespace CurrencyData.Infrastructure.Repositories.Abstractions
{
    public interface ICurrencyDataRepository
    {
        Task<ResponseData> GetAsync(QueryParameters queryParameters);
    }
}
