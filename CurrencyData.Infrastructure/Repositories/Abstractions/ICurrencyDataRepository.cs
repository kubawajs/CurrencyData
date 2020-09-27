using System;
using System.Threading.Tasks;
using CurrencyData.Infrastructure.Domain;

namespace CurrencyData.Infrastructure.Repositories.Abstractions
{
    public interface ICurrencyDataRepository
    {
        Task<ResponseData> GetAsync(string inCurrency, string outCurrency, DateTime startDate, DateTime endDate);
    }
}
