using System;
using System.Globalization;
using System.Linq;
using AutoMapper;
using CurrencyData.Infrastructure.Domain;
using EcbSdmx.Core.Domain.Response;

namespace CurrencyData.Infrastructure.Mapping
{
    public class MappingProfile : Profile
    {
        // TODO: to other file
        public const string Currency = "CURRENCY";
        public const string CurrencyDenom = "CURRENCY_DENOM";

        public MappingProfile()
        {
            CreateMap<EcbSdmx.Core.Domain.Response.ApiResponseData, ResponseData>()
                .ForMember(dest => dest.FromCurrency,
                    opt 
                        => opt.MapFrom(src => src.DataSet.Series.SeriesKey.Value.First(x => x.Id == Currency).Value))
                .ForMember(dest => dest.ToCurrency,
                    opt 
                        => opt.MapFrom(src => src.DataSet.Series.SeriesKey.Value.First(x => x.Id == CurrencyDenom).Value))
                .ForMember(dest => dest.StartDate,
                    opt => opt.MapFrom(src => Convert.ToDateTime(src.DataSet.Series.Observations.First().Dimension.Value)))
                .ForMember(dest => dest.EndDate,
                    opt => opt.MapFrom(src => Convert.ToDateTime(src.DataSet.Series.Observations.Last().Dimension.Value)))
                .ForMember(dest => dest.ExchangeRates,
                    opt => opt.MapFrom(src => src.DataSet.Series.Observations));

            CreateMap<Observation, DailyExchangeRate>()
                .ForMember(dest => dest.Date, opt
                    => opt.MapFrom(src => Convert.ToDateTime(src.Dimension.Value)))
                .ForMember(dest => dest.Rate, opt 
                    => opt.MapFrom(src => src.Value.Value));
        }
    }
}
