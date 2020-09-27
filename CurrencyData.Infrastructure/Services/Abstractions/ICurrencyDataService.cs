using CurrencyData.Infrastructure.Domain;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Services.Abstractions
{
    public interface ICurrencyDataService
    {
        Task<ResponseData> GetCurrencies(QueryParameters queryParameters);
    }
}
