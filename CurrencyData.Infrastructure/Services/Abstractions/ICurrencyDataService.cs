using CurrencyData.Infrastructure.Domain;
using System;
using System.Threading.Tasks;

namespace CurrencyData.Infrastructure.Services.Abstractions
{
    public interface ICurrencyDataService
    {
        Task<ResponseData> GetCurrencies(string inCode, string outCode, DateTime startDate, DateTime endDate);
    }
}
